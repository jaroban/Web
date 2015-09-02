using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public static class Database
{
    public static string ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            //return @"Data Source=localhost\SQLEXPRESS;Initial Catalog=kemp;Integrated Security=true;";
            //return @"Server=localhost;Database=MSSQLSERVER;Initial Catalog=kemp;Integrated Security=true;";
        }
    }

    public static List<Email> GetEmails()
    {
        var result = new List<Email>();

        using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "GetEmails";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = sqlConnection;

                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32(0);
                        var address = reader.GetString(1);
                        var subject = reader.GetString(2);
                        var body = reader.GetString(3);
                        int? variabilny = reader.IsDBNull(4) ? (int?)null : reader.GetInt32(4);
                        if (variabilny.HasValue) body = string.Format(body, variabilny.Value);
                        result.Add(new Email(id, address, subject, body));
                    }
                }
            }
        }
        return result;
    }

    public static DataTable GetDataTable(List<Email> data)
    {
        var dt = new DataTable("Emails");

        var colId = new DataColumn("id", typeof(int));
        var colAddress = new DataColumn("address", typeof(string));
        var colSubject = new DataColumn("subject", typeof(string));
        var colBody = new DataColumn("body", typeof(string));
        var colSuccess = new DataColumn("success", typeof(bool));
        var colResult = new DataColumn("result", typeof(string));

        dt.Columns.Add(colId);
        dt.Columns.Add(colAddress);
        dt.Columns.Add(colSubject);
        dt.Columns.Add(colBody);
        dt.Columns.Add(colSuccess);
        dt.Columns.Add(colResult);

        foreach (var entry in data)
        {
            var row = dt.NewRow();

            row[colId] = entry.Id;
            row[colSuccess] = entry.Success;
            row[colResult] = entry.Result;

            dt.Rows.Add(row);
        }
        return dt;
    }

    public static void UpdateEmails(List<Email> emails)
    {
        using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.CommandText = "UpdateEmails";
                command.CommandType = CommandType.StoredProcedure;
                command.Connection = sqlConnection;

                var parameter1 = command.Parameters.AddWithValue("@emails", Database.GetDataTable(emails));
                parameter1.SqlDbType = SqlDbType.Structured;
                parameter1.TypeName = "dbo.ListOfEmails";

                sqlConnection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}