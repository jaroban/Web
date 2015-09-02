using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class Email
{
    public string Subject;
    public string Address;
    public string Body;

    public Email(string address, string subject, string body)
    {
        Address = address;
        Subject = subject;
        Body = body;
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

            row[colAddress] = entry.Address;
            row[colSubject] = entry.Subject;
            row[colBody] = entry.Body;

            dt.Rows.Add(row);
        }
        return dt;
    }
}