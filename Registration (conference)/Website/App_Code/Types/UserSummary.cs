using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public struct UserSummary
{
    public int Id { get; set; }
    public int IdVariabilny { get; set; }
    public string Meno { get; set; }
    public string Priezvisko { get; set; }
    public string Zbor { get; set; }
    public string Tricko { get; set; }
    public float Preplatok { get; set; }
    public bool Prisli { get; set; }
}
