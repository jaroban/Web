using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Configuration;

public static class Database
{
    public static string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            // T420:
            //return @"Server=localhost;Database=MSSQLSERVER;Initial Catalog=konfera;Integrated Security=true;";
            // X201:
            //return @"Data Source=localhost\SQLEXPRESS;Initial Catalog=kemp;Integrated Security=true;";
        }
    }

    public static RegistrationResult WriteData(List<RegistrationEntry> data, List<Email> emails,
        string payerEmail, float amountToPay)
    {
        var success = false;
        var badEmails = new List<string>();

        try
        {
            using (var transaction = new TransactionScope())
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.PrepareStoredProcedure(sqlConnection, "AddUsers");
                        command.AddParameterUserDefined("@users", "dbo.ListOfUsers", RegistrationEntry.GetDataTable(data));
                        command.AddParameterUserDefined("@emails", "dbo.ListOfEmails", Email.GetDataTable(emails));
                        command.AddParameterString("@payerEmail", payerEmail);
                        command.AddParameterFloat("@amount", amountToPay);

                        sqlConnection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                success = reader.GetInt32(0) > 0;
                            }
                            reader.NextResult();

                            if (!success)
                            {
                                while (reader.Read())
                                {
                                    badEmails.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
                transaction.Complete();
            }
        }
        catch (Exception ex)
        {

        }
        return new RegistrationResult
        {
            Success = success,
            AlreadyRegisteredEmails = badEmails
        };
    }

    public static SummaryData GetSummary()
    {
        var result = new SummaryData 
        { 
            Sluzby = new List<IdName>()
        };

        using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.PrepareStoredProcedure(sqlConnection, "GetSummary");

                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        result.TotalPeople = reader.GetInt32(0);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        result.Sluzby.Add(new IdName
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
        }
        return result;
    }
}