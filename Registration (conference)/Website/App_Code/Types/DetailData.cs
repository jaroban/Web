using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

[Serializable]
public class DetailData
{
    public int? Id;
    public string Meno;
    public string Priezvisko;
    public string Email;
    public string Telefon;
    public int? IdZbor;
    public string InyZbor;
    public bool PiatokVecera;
    public bool PiatokVecera2;
    public bool UbytovaniePiatokSobota;
    public bool TichaTriedaPiatokSobota;
    public bool SobotaRanajky;
    public bool SobotaObed;
    public bool SobotaVecera;
    public bool SobotaVecera2;
    public bool UbytovanieSobotaNedela;
    public bool TichaTriedaSobotaNedela;
    public bool NedelaRanajky;
    public bool NedelaObed;
    public bool Sach;
    public bool PingPong;
    public int? IdTricko;
    public bool Dobrovolnik;
    public string Poznamka;
    public DateTime? DtRegistered;
    public bool Internat1;
    public bool Internat2;
    public float? CashBack;
    public bool InternatZadarmo;
    public bool RegistraciaZadarmo;
    public bool JedloZadarmo;
    public DateTime? DtPlatba;
    public bool Prisli;
    public float Suma;
    public bool IstoPride;
    public float Zaplatili;
    public float SkupinaDlzi;
    public float Dar;
    public float Preplatok;

    public List<IdName> Group;
}
