using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

public static class Database
{
    public static string ConnectionString
    {
        get
        {
            // Data Source=(local)\MSSQLSERVER
            return @"Server=localhost;Database=MSSQLSERVER;Initial Catalog=konfera;Integrated Security=true;";
        }
    }

    public static RegistrationResult WriteData(List<RegistrationEntry> data, List<Email> emails,
        string payerEmail, float amountToPay, float donation, int idCurrency, bool needHelp)
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
                        command.PrepareStoredProcedure(sqlConnection, "AddUsers4");
                        command.AddParameterUserDefined("@users", "dbo.ListOfUsers", RegistrationEntry.GetDataTable(data));
                        command.AddParameterUserDefined("@emails", "dbo.ListOfEmails", Email.GetDataTable(emails));
                        command.AddParameterString("@payerEmail", payerEmail);
                        command.AddParameterFloat("@amount", amountToPay);
                        command.AddParameterFloat("@donation", donation);
                        command.AddParameterInt("@idCurrency", idCurrency);
                        command.AddParameterBool("@needHelp", needHelp);

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
        catch(Exception ex)
        {

        }
        return new RegistrationResult
        {
            Success = success,
            AlreadyRegisteredEmails = badEmails
        };
    }

    public static void RegisterSportsTeam(int idSport, string name,
        string player1, string player2, string player3, string player4, string player5, string player6, string player7)
    {
        try
        {
            using (var transaction = new TransactionScope())
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.PrepareStoredProcedure(sqlConnection, "AddTeam");
                        command.AddParameterInt("@idSport", idSport);

                        command.AddParameterString("@name", name);
                        command.AddParameterString("@player1", player1);
                        command.AddParameterString("@player2", player2);
                        command.AddParameterString("@player3", player3);
                        command.AddParameterString("@player4", player4);
                        command.AddParameterString("@player5", player5);
                        command.AddParameterString("@player6", player6);
                        command.AddParameterString("@player7", player7);

                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Complete();
            }
        }
        catch(Exception ex)
        {

        }
    }

    public static SummaryData GetSummary()
    {
        var result = new SummaryData 
        { 
            Tricka = new List<TeeShirtInfo>(),
            Volunteers = new List<UserInfo>(),
            Commenters = new List<UserInfo>(),
            NeedHelp = new List<UserInfo>(),
            Churches = new List<NameCount>()
        };

        using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.PrepareStoredProcedure(sqlConnection, "GetSummary3");

                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        var i = 0;
                        result.TotalPeople = reader.GetInt32(i++);
                        result.PiatokVecera = reader.GetInt32(i++);
                        result.PiatokVeceraZaplatene = reader.GetInt32(i++);
                        result.PiatokVecera2 = reader.GetInt32(i++);
                        result.PiatokVecera2Zaplatene = reader.GetInt32(i++);
                        result.UbytovaniePiatokSobota = reader.GetInt32(i++);
                        result.TichaTriedaPiatokSobota = reader.GetInt32(i++);
                        result.SobotaRanajky = reader.GetInt32(i++);
                        result.SobotaRanajkyZaplatene = reader.GetInt32(i++);
                        result.SobotaObed = reader.GetInt32(i++);
                        result.SobotaObedZaplatene = reader.GetInt32(i++);
                        result.SobotaVecera = reader.GetInt32(i++);
                        result.SobotaVeceraZaplatene = reader.GetInt32(i++);
                        result.SobotaVecera2 = reader.GetInt32(i++);
                        result.SobotaVecera2Zaplatene = reader.GetInt32(i++);
                        result.UbytovanieSobotaNedela = reader.GetInt32(i++);
                        result.TichaTriedaSobotaNedela = reader.GetInt32(i++);
                        result.NedelaRanajky = reader.GetInt32(i++);
                        result.NedelaRanajkyZaplatene = reader.GetInt32(i++);
                        result.NedelaObed = reader.GetInt32(i++);
                        result.NedelaObedZaplatene = reader.GetInt32(i++);
                        result.Sach = reader.GetInt32(i++);
                        result.PingPong = reader.GetInt32(i++);
                        result.VolunteersTotal = reader.GetInt32(i++);
                        result.Internat1 = reader.GetInt32(i++);
                        result.Internat2 = reader.GetInt32(i++);
                    }
                    reader.NextResult();

                    int totalOrdered = 0;
                    while (reader.Read())
                    {
                        var ordered = reader.GetInt32(1);
                        result.Tricka.Add(new TeeShirtInfo
                        {
                            Name = reader.GetString(0),
                            Ordered = ordered
                        });
                        totalOrdered += ordered;
                    }
                    reader.NextResult();

                    int totalPaid = 0;
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var paid = reader.GetInt32(1);
                        var tee = result.Tricka.First(x => x.Name == name);
                        if(tee != null) tee.Paid = paid;
                        totalPaid += paid;
                    }
                    reader.NextResult();
                    result.Tricka.Add(new TeeShirtInfo
                        {
                            Name = "Spolu",
                            Ordered = totalOrdered,
                            Paid = totalPaid
                        });

                    if (reader.Read()) result.ExpectingEur = (float)reader.GetDecimal(0);
                    reader.NextResult();

                    if (reader.Read()) result.ExpectingCzk = (float)reader.GetDecimal(0);
                    reader.NextResult();

                    if (reader.Read()) result.MoneyFromPeople = (float)reader.GetDecimal(0);
                    reader.NextResult();

                    if (reader.Read()) result.MoneyFromChurches = (float)reader.GetDouble(0);
                    reader.NextResult();

                    while (reader.Read())
                    {
                        result.Volunteers.Add(new UserInfo
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            Email = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Note = reader.GetString(4)
                        });
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        result.Commenters.Add(new UserInfo
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            Email = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Note = reader.GetString(4)
                        });
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        result.NeedHelp.Add(new UserInfo
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            Email = reader.GetString(2),
                            Phone = reader.GetString(3),
                            Note = reader.GetString(4)
                        });
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        result.Churches.Add(new NameCount
                        {
                            Name = reader.GetString(0),
                            Count = reader.GetInt32(1)
                        });
                    }
                }
            }
        }
        return result;
    }

    public static DetailData GetDetail(int idUser)
    {
        var result = new DetailData { Group = new List<IdName>() };

        using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.PrepareStoredProcedure(sqlConnection, "GetDetail");
                command.AddParameterInt("@idUser", idUser);

                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var i = 0;
                        result.Id = reader.GetInt32(i++);
                        result.Meno = reader.GetString(i++);
                        result.Priezvisko = reader.GetString(i++);
                        result.Email = reader.GetString(i++);
                        result.Telefon = reader.GetString(i++);
                        result.IdZbor = reader.IsDBNull(i) ? (int?)null : reader.GetInt32(i); i++;
                        result.InyZbor = reader.IsDBNull(i) ? null : reader.GetString(i); i++;
                        result.PiatokVecera = reader.GetBoolean(i++);
                        result.PiatokVecera2 = reader.GetInt32(i++) > 0;
                        result.UbytovaniePiatokSobota = reader.GetBoolean(i++);
                        result.TichaTriedaPiatokSobota = reader.GetBoolean(i++);
                        result.SobotaRanajky = reader.GetBoolean(i++);
                        result.SobotaObed = reader.GetBoolean(i++);
                        result.SobotaVecera = reader.GetBoolean(i++);
                        result.SobotaVecera2 = reader.GetInt32(i++) > 0;
                        result.UbytovanieSobotaNedela = reader.GetBoolean(i++);
                        result.TichaTriedaSobotaNedela = reader.GetBoolean(i++);
                        result.NedelaRanajky = reader.GetBoolean(i++);
                        result.NedelaObed = reader.GetBoolean(i++);
                        result.Sach = reader.GetBoolean(i++);
                        result.PingPong = reader.GetBoolean(i++);
                        result.IdTricko = reader.IsDBNull(i) ? (int?)null : reader.GetInt32(i); i++;
                        result.Dobrovolnik = reader.GetBoolean(i++);
                        result.Poznamka = reader.GetString(i++);
                        result.DtRegistered = reader.IsDBNull(i) ? (DateTime?)null : reader.GetDateTime(i); i++;
                        result.Internat1 = reader.GetBoolean(i++);
                        result.Internat2 = reader.GetBoolean(i++);
                        result.CashBack = (float)reader.GetDecimal(i++);
                        result.InternatZadarmo = reader.GetBoolean(i++);
                        result.RegistraciaZadarmo = reader.GetBoolean(i++);
                        result.JedloZadarmo = reader.GetBoolean(i++);
                        result.DtPlatba = reader.IsDBNull(i) ? (DateTime?)null : reader.GetDateTime(i); i++;
                        result.Prisli = reader.GetBoolean(i++);
                        result.Suma = (float)reader.GetDecimal(i++);
                        result.IstoPride = reader.GetBoolean(i++);
                        result.Zaplatili = (float)reader.GetDecimal(i++);
                        result.SkupinaDlzi = (float)reader.GetDecimal(i++);
                        result.Dar = (float)reader.GetDecimal(i++);
                        result.Preplatok = (float)reader.GetDecimal(i++);
                    }
                    reader.NextResult();

                    result.Group = reader.GetListOfIdNames();
                }
            }
        }
        return result;
    }                              

    public static int? UpdateOrCreate(DetailData data)
    {
        int? idUser;
        using (var transaction = new TransactionScope())
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.PrepareStoredProcedure(sqlConnection, "UpdateOrAddUser");
                    command.AddParameterInt("@idUser", data.Id);
                    command.AddParameterString("@Meno", data.Meno);
                    command.AddParameterString("@Priezvisko", data.Priezvisko);
                    command.AddParameterString("@Email", data.Email);
                    command.AddParameterString("@Telefon", data.Telefon);
                    command.AddParameterInt("@IdZbor", data.IdZbor);
                    command.AddParameterString("@InyZbor", data.InyZbor);

                    command.AddParameterBool("@PiatokVecera", data.PiatokVecera);
                    command.AddParameterBool("@PiatokVecera2", data.PiatokVecera2);
                    command.AddParameterBool("@UbytovaniePiatokSobota", data.UbytovaniePiatokSobota);
                    command.AddParameterBool("@TichaTriedaPiatokSobota", data.TichaTriedaPiatokSobota);

                    command.AddParameterBool("@SobotaRanajky", data.SobotaRanajky);
                    command.AddParameterBool("@SobotaObed", data.SobotaObed);
                    command.AddParameterBool("@SobotaVecera", data.SobotaVecera);
                    command.AddParameterBool("@SobotaVecera2", data.SobotaVecera2);
                    command.AddParameterBool("@UbytovanieSobotaNedela", data.UbytovanieSobotaNedela);
                    command.AddParameterBool("@TichaTriedaSobotaNedela", data.TichaTriedaSobotaNedela);

                    command.AddParameterBool("@NedelaRanajky", data.NedelaRanajky);
                    command.AddParameterBool("@NedelaObed", data.NedelaObed);

                    command.AddParameterBool("@Sach", data.Sach);
                    command.AddParameterBool("@PingPong", data.PingPong);
                    command.AddParameterInt("@IdTricko", data.IdTricko);
                    command.AddParameterBool("@Sluziaci", data.Dobrovolnik);
                    command.AddParameterString("@Poznamka", data.Poznamka);

                    command.AddParameterBool("@Internat1", data.Internat1);
                    command.AddParameterBool("@Internat2", data.Internat2);
                    command.AddParameterFloat("@Preplacame", data.CashBack);
                    command.AddParameterBool("@InternatZadarmo", data.InternatZadarmo);
                    command.AddParameterBool("@RegistraciaZadarmo", data.RegistraciaZadarmo);
                    command.AddParameterBool("@JedloZadarmo", data.JedloZadarmo);

                    command.AddParameterBool("@Prisli", data.Prisli);
                    command.AddParameterBool("@IstoPride", data.IstoPride);
                    command.AddParameterFloat("@Donation", data.Dar);

                    command.AddOutputParameterInt("@NewId");

                    sqlConnection.Open();
                    command.ExecuteNonQuery();

                    idUser = command.Parameters["@NewId"].Value as int?;
                }
            }
            transaction.Complete();
        }
        return idUser;
    }

    public static List<IdName> GetFilter()
    {
        var result = new List<IdName>();

        using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.PrepareStoredProcedure(sqlConnection, "GetFilter");

                sqlConnection.Open();
                using (var reader = command.ExecuteReader())
                {
                    result = reader.GetListOfIdNames();
                }
            }
        }
        return result;
    }

    public static List<UserSummary> GetList(string name, int idChurch, string from, string to, bool notArrived)
    {
        var result = new List<UserSummary>();

        using (var sqlConnection = new SqlConnection(ConnectionString))
        {
            using (var command = sqlConnection.CreateCommand())
            {
                command.PrepareStoredProcedure(sqlConnection, "GetList");
                command.AddParameterString("@name", name);
                command.AddParameterInt("@idChurch", idChurch);
                command.AddParameterString("@from", from);
                command.AddParameterString("@to", to);
                command.AddParameterBool("@notArrived", notArrived);

                sqlConnection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new UserSummary
                        {
                            Id = reader.GetInt32(0),
                            IdVariabilny = reader.GetInt32(1),
                            Meno = reader.IsDBNull(2) ? null : reader.GetString(2),
                            Priezvisko = reader.IsDBNull(3) ? null : reader.GetString(3),
                            Zbor = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Tricko = reader.IsDBNull(5) ? null : reader.GetString(5),
                            Preplatok = (float)reader.GetDecimal(6),
                            Prisli = reader.IsDBNull(7) ? false : reader.GetBoolean(7)
                        });
                    }
                }
            }
        }
        return result;
    }

    public static void AddDonation(int idUser, float amount)
    {
        using (var transaction = new TransactionScope())
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.PrepareStoredProcedure(sqlConnection, "AddDonation2");
                    command.AddParameterInt("@idUser", idUser);
                    command.AddParameterFloat("@amount", amount);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            transaction.Complete();
        }
    }

    public static void ShowedUp(int idUser)
    {
        try
        {
            using (var transaction = new TransactionScope())
            {
                using (var sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (var command = sqlConnection.CreateCommand())
                    {
                        command.PrepareStoredProcedure(sqlConnection, "ShowedUp");
                        command.AddParameterInt("@id", idUser);

                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                transaction.Complete();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public static void AddPayment(int idUser, float amount, string note)
    {
        using (var transaction = new TransactionScope())
        {
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.PrepareStoredProcedure(sqlConnection, "AddPayment2");
                    command.AddParameterInt("@idUser", idUser);
                    command.AddParameterFloat("@amount", amount);
                    command.AddParameterString("@note", note);

                    sqlConnection.Open();
                    command.ExecuteNonQuery();
                }
            }
            transaction.Complete();
        }
    }
}