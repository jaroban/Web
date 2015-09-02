using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

[Serializable]
public class RegistrationEntry
{
    public int? Id { get; set; }
    public string Meno { get; set; }
    public string Priezvisko { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public int IdSluzba { get; set; }
    public string InaSluzba { get; set; }
    public string Poznamka { get; set; }

    public bool Single { get; set; }
    public List<string> Errors { get; set; }
    public string ErrorString { get { return Errors == null || Errors.Count == 0 ? "OK" : string.Join("<br/>", Errors); } }
    public bool Valid { get { return Errors != null && Errors.Count == 0; } }
    public string CssClass { get { return Errors != null && Errors.Count == 0 ? "valid" : "error"; } }

    public float RegistrationFee
    {
        get
        {
            return DateTime.Now < Prices.DeadLine1 ? Prices.RegistracnyPoplatok1 : Prices.RegistracnyPoplatok2;
        }
    }

    public string CostString
    {
        get
        {
            return Common.FormatMoney(RegistrationFee);
        }
    }

    public string Title
    {
        get
        {
            if (string.IsNullOrWhiteSpace(Meno) && string.IsNullOrWhiteSpace(Priezvisko))
            {
                return string.Format("Anonym {0}", Id + 1);
            }
            return Meno + " " + Priezvisko;
        }
    }

    public static DataTable GetDataTable(List<RegistrationEntry> data)
    {
        var dt = new DataTable("Users");

        var colMeno = new DataColumn("Meno", typeof(string));
        var colPriezvisko = new DataColumn("Priezvisko", typeof(string));
        var colEmail = new DataColumn("Email", typeof(string));
        var colTelefon = new DataColumn("Telefon", typeof(string));
        var colIdSluzba = new DataColumn("IdSluzba", typeof(int));
        var colInaSluzba = new DataColumn("InaSluzba", typeof(string));
        var colPoznamka = new DataColumn("Poznamka", typeof(string));

        dt.Columns.Add(colMeno);
        dt.Columns.Add(colPriezvisko);
        dt.Columns.Add(colEmail);
        dt.Columns.Add(colTelefon);
        dt.Columns.Add(colIdSluzba);
        dt.Columns.Add(colInaSluzba);
        dt.Columns.Add(colPoznamka);

        foreach (var entry in data)
        {
            var row = dt.NewRow();

            row[colMeno] = entry.Meno;
            row[colPriezvisko] = entry.Priezvisko;
            row[colEmail] = entry.Email;
            row[colTelefon] = entry.Telefon;
            row[colIdSluzba] = entry.IdSluzba > 0 ? (object)entry.IdSluzba : DBNull.Value;
            row[colInaSluzba] = entry.InaSluzba;
            row[colPoznamka] = entry.Poznamka;

            dt.Rows.Add(row);
        }
        return dt;
    }
}
