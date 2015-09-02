using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Prices
{
    public const float Ranajky = 1;
    public const float Obed = 2.5f;
    public const float Vecera = 2.2f;
    public const float Vecera2 = 1;
    public const float Ubytovanie = 1;
    public const float Internat1 = 10;
    public const float Internat2 = 14;
    public const float RegistracnyPoplatok1 = 8;
    public const float RegistracnyPoplatokDobrovolnik = 4;
    public static DateTime DeadLine1 { get { return new DateTime(2015, 1, 16, 23, 59, 59); } }
    public const float RegistracnyPoplatok2 = 11;
    public static DateTime DeadLine2 { get { return new DateTime(2015, 2, 10, 23, 59, 59); } }
}