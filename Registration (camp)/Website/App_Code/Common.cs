using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public class Common
{
    public const int SpaceInside = 59;
    public const int SpaceOutside = 74;

    public const string ChybaMeno = "Prosím zadaj meno";
    public const string ChybaPriezvisko = "Prosím vyplň priezvisko";
    public const string ChybaEmail = "Prosím vyplň email";
    public const string ChybaTelefon = "Prosím vyplň telefónne číslo";
    public const string ChybaSluzba = "Prosím vyber službu";
    public const string ChybaCaptcha = "To nevyzerá správne";
    public const string ChybaEmailSaOpakuje = "Tento email sa opakuje";
    public const string ChybaEmailUzZaregistrovany = "Osoba s týmto emailom je už zaregistrovaná";

    private static Regex _validEmail = new Regex(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", RegexOptions.IgnoreCase);
    private static Regex _validPhoneNumber = new Regex(@"^\+?[\d\s]{6,}$");

    public static bool ValidatePhoneNumber(string phoneNumber)
    {
        return !string.IsNullOrEmpty(phoneNumber) && _validPhoneNumber.IsMatch(phoneNumber);
    }

    public static bool ValidateEmail(string email)
    {
        return !string.IsNullOrEmpty(email) && _validEmail.IsMatch(email);
    }

    public static string FormatMoney(float amount)
    {
        return string.Format("{0:0.00} EUR", amount);
    }
}