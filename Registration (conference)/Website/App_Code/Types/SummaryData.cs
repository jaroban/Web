using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

[Serializable]
public class SummaryData
{
    public int TotalPeople;
    public int PiatokVecera;
    public int PiatokVecera2;
    public int PiatokVeceraZaplatene;
    public int PiatokVecera2Zaplatene;
    public int UbytovaniePiatokSobota;
    public int TichaTriedaPiatokSobota;
    public int SobotaRanajky;
    public int SobotaObed;
    public int SobotaVecera;
    public int SobotaVecera2;
    public int SobotaRanajkyZaplatene;
    public int SobotaObedZaplatene;
    public int SobotaVeceraZaplatene;
    public int SobotaVecera2Zaplatene;
    public int UbytovanieSobotaNedela;
    public int TichaTriedaSobotaNedela;
    public int NedelaRanajky;
    public int NedelaObed;
    public int NedelaRanajkyZaplatene;
    public int NedelaObedZaplatene;
    public int Sach;
    public int PingPong;
    public int VolunteersTotal;
    public int Internat1;
    public int Internat2;

    public float ExpectingEur;
    public float ExpectingCzk;
    public float MoneyFromPeople;
    public float MoneyFromChurches;

    public List<TeeShirtInfo> Tricka;
    public List<UserInfo> Volunteers;
    public List<UserInfo> Commenters;
    public List<UserInfo> NeedHelp;
    public List<NameCount> Churches;
}
