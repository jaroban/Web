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
    public List<IdName> Sluzby;
}
