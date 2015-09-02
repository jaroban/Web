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
    public int IdZbor { get; set; }
    public string InyZbor { get; set; }
    public bool PiatokVecera { get; set; }
    public bool PiatokVecera2 { get; set; }
    public bool UbytovaniePiatokSobota { get; set; }
    public bool TichaTriedaPiatokSobota { get; set; }
    public bool SobotaRanajky { get; set; }
    public bool SobotaObed { get; set; }
    public bool SobotaVecera { get; set; }
    public bool SobotaVecera2 { get; set; }
    public bool UbytovanieSobotaNedela { get; set; }
    public bool TichaTriedaSobotaNedela { get; set; }
    public bool NedelaRanajky { get; set; }
    public bool NedelaObed { get; set; }
    public bool Sach { get; set; }
    public bool PingPong { get; set; }
    public int IdTricko { get; set; }
    public string Tricko { get; set; }
    public bool Sluziaci { get; set; }
    public string Poznamka { get; set; }

    public bool Single { get; set; }
    public int IdCurrency { get; set; }
    public List<string> Errors { get; set; }
    public string ErrorString { get { return Errors == null || Errors.Count == 0 ? "OK" : string.Join("<br/>", Errors); } }
    public bool Valid { get { return Errors != null && Errors.Count == 0; } }
    public string CssClass { get { return Errors != null && Errors.Count == 0 ? "valid" : "error"; } }

    /*
    public RegistrationEntry(int id)
    {
        Id = id;
    }
    */
    public float RegistrationFee
    {
        get
        {
            if (DateTime.Now < Prices.DeadLine1)
            {
                return Sluziaci ? Prices.RegistracnyPoplatokDobrovolnik : Prices.RegistracnyPoplatok1;
            }
            return Prices.RegistracnyPoplatok2;
        }
    }

    public float Cost
    {
        get
        {
            var sum = RegistrationFee;

            if (PiatokVecera) sum += Prices.Vecera;
            if (PiatokVecera2) sum += Prices.Vecera2;
            if (UbytovaniePiatokSobota) sum += Prices.Ubytovanie;
            if (SobotaRanajky) sum += Prices.Ranajky;
            if (SobotaObed) sum += Prices.Obed;
            if (SobotaVecera) sum += Prices.Vecera;
            if (SobotaVecera2) sum += Prices.Vecera2;
            if (UbytovanieSobotaNedela) sum += Prices.Ubytovanie;
            if (NedelaRanajky) sum += Prices.Ranajky;
            if (NedelaObed) sum += Prices.Obed;

            return sum;
        }
    }

    public string CostString
    {
        get
        {
            return Common.FormatMoney(Cost, IdCurrency);
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
        var colIdZbor = new DataColumn("IdZbor", typeof(int));
        var colInyZbor = new DataColumn("InyZbor", typeof(string));
        var colPiatokVecera = new DataColumn("PiatokVecera", typeof(bool));
        var colPiatokVecera2 = new DataColumn("PiatokVecera2", typeof(bool));
        var colUbytovaniePiatokSobota = new DataColumn("UbytovaniePiatokSobota", typeof(bool));
        var colTichaTriedaPiatokSobota = new DataColumn("TichaTriedaPiatokSobota", typeof(bool));
        var colSobotaRanajky = new DataColumn("SobotaRanajky", typeof(bool));
        var colSobotaObed = new DataColumn("SobotaObed", typeof(bool));
        var colSobotaVecera = new DataColumn("SobotaVecera", typeof(bool));
        var colSobotaVecera2 = new DataColumn("SobotaVecera2", typeof(bool));
        var colUbytovanieSobotaNedela = new DataColumn("UbytovanieSobotaNedela", typeof(bool));
        var colTichaTriedaSobotaNedela = new DataColumn("TichaTriedaSobotaNedela", typeof(bool));
        var colNedelaRanajky = new DataColumn("NedelaRanajky", typeof(bool));
        var colNedelaObed = new DataColumn("NedelaObed", typeof(bool));
        var colSach = new DataColumn("Sach", typeof(bool));
        var colPingPong = new DataColumn("PingPong", typeof(bool));
        var colIdTricko = new DataColumn("IdTricko", typeof(int));
        var colSluziaci = new DataColumn("Sluziaci", typeof(string));
        var colPoznamka = new DataColumn("Poznamka", typeof(string));

        dt.Columns.Add(colMeno);
        dt.Columns.Add(colPriezvisko);
        dt.Columns.Add(colEmail);
        dt.Columns.Add(colTelefon);
        dt.Columns.Add(colIdZbor);
        dt.Columns.Add(colInyZbor);
        dt.Columns.Add(colPiatokVecera);
        dt.Columns.Add(colPiatokVecera2);
        dt.Columns.Add(colUbytovaniePiatokSobota);
        dt.Columns.Add(colTichaTriedaPiatokSobota);
        dt.Columns.Add(colSobotaRanajky);
        dt.Columns.Add(colSobotaObed);
        dt.Columns.Add(colSobotaVecera);
        dt.Columns.Add(colSobotaVecera2);
        dt.Columns.Add(colUbytovanieSobotaNedela);
        dt.Columns.Add(colTichaTriedaSobotaNedela);
        dt.Columns.Add(colNedelaRanajky);
        dt.Columns.Add(colNedelaObed);
        dt.Columns.Add(colSach);
        dt.Columns.Add(colPingPong);
        dt.Columns.Add(colIdTricko);
        dt.Columns.Add(colSluziaci);
        dt.Columns.Add(colPoznamka);

        foreach (var entry in data)
        {
            var row = dt.NewRow();

            row[colMeno] = entry.Meno;
            row[colPriezvisko] = entry.Priezvisko;
            row[colEmail] = entry.Email;
            row[colTelefon] = entry.Telefon;
            row[colIdZbor] = entry.IdZbor > 0 ? (object)entry.IdZbor : DBNull.Value;
            row[colInyZbor] = entry.InyZbor;
            row[colPiatokVecera] = entry.PiatokVecera;
            row[colPiatokVecera2] = entry.PiatokVecera2;
            row[colUbytovaniePiatokSobota] = entry.UbytovaniePiatokSobota;
            row[colTichaTriedaPiatokSobota] = entry.TichaTriedaPiatokSobota;
            row[colSobotaRanajky] = entry.SobotaRanajky;
            row[colSobotaObed] = entry.SobotaObed;
            row[colSobotaVecera] = entry.SobotaVecera;
            row[colSobotaVecera2] = entry.SobotaVecera2;
            row[colUbytovanieSobotaNedela] = entry.UbytovanieSobotaNedela;
            row[colTichaTriedaSobotaNedela] = entry.TichaTriedaSobotaNedela;
            row[colNedelaRanajky] = entry.NedelaRanajky;
            row[colNedelaObed] = entry.NedelaObed;
            row[colSach] = entry.Sach;
            row[colPingPong] = entry.PingPong;
            row[colIdTricko] = entry.IdTricko > 0 ? (object)entry.IdTricko : DBNull.Value;
            row[colSluziaci] = entry.Sluziaci ? "Dobrovolnik" : "";
            row[colPoznamka] = entry.Poznamka;

            dt.Rows.Add(row);
        }
        return dt;
    }
}
