using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public class Common
{
    public const float ExchangeRateCZK = 27.61f;

    public const string ChybaMeno = "Prosím zadajte meno";
    public const string ChybaPriezvisko = "Prosím vyplňte priezvisko";
    public const string ChybaEmail = "Prosím vyplňte email";
    public const string ChybaTelefon = "Prosím vyplňte telefónne číslo";
    public const string ChybaZbor = "Prosím vyberte zbor";
    public const string ChybaSponzorskyDar = "Prosím zadajte dar ako číslo";
    public const string ChybaPreplacame = "Prosím zadajte preplatenú sumu ako číslo";
    public const string ChybaDoplatili = "Prosím zadajte doplatenú sumu ako číslo";
    public const string ChybaMenoTimu = "Prosím zadajte meno tímu";
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

    public static string FormatMoney(float amount, int idCurrency)
    {
        return string.Format("{0:0.00} {1}", amount * 
            (idCurrency == 1 ? 1 : Common.ExchangeRateCZK), 
            (idCurrency == 1 ? "EUR" : "CZK"));
    }

    public static string GetIpAddress()
    {
        if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }
        if (HttpContext.Current.Request.UserHostAddress.Length != 0)
        {
            return HttpContext.Current.Request.UserHostAddress;
        }
        return "Unknown IP address";
    }

    public static void FillTeeShirts(DropDownList ddlTricko)
    {
        ddlTricko.Items.Clear();

        ddlTricko.Items.Add(new ListItem("Žiadne", "0"));
        ddlTricko.Items.Add(new ListItem("Dámske - S", "1"));
        ddlTricko.Items.Add(new ListItem("Dámske - M", "2"));
        ddlTricko.Items.Add(new ListItem("Dámske - L", "3"));
        ddlTricko.Items.Add(new ListItem("Dámske - XL", "4"));
        ddlTricko.Items.Add(new ListItem("Dámske - XXL", "5"));
        ddlTricko.Items.Add(new ListItem("Pánske - S", "6"));
        ddlTricko.Items.Add(new ListItem("Pánske - M", "7"));
        ddlTricko.Items.Add(new ListItem("Pánske - L", "8"));
        ddlTricko.Items.Add(new ListItem("Pánske - XL", "9"));
        ddlTricko.Items.Add(new ListItem("Pánske - XXL", "10"));
    }

    public static void FillChurches(DropDownList ddlZbor)
    {
        ddlZbor.Items.Clear();

        ddlZbor.Items.Add(new ListItem("Prosím vyberte zbor", "0"));
        ddlZbor.Items.Add(new ListItem("Aš", "1"));
        ddlZbor.Items.Add(new ListItem("Banská Bystrica", "2"));
        ddlZbor.Items.Add(new ListItem("Banská Štiavnica", "3"));
        ddlZbor.Items.Add(new ListItem("Bernolákovo", "4"));
        ddlZbor.Items.Add(new ListItem("Blansko", "5"));
        ddlZbor.Items.Add(new ListItem("Bratislava - IBCB", "6"));
        ddlZbor.Items.Add(new ListItem("Bratislava I. - Palisády", "7"));
        ddlZbor.Items.Add(new ListItem("Bratislava II. - Podunajské Biskupice", "8"));
        ddlZbor.Items.Add(new ListItem("Bratislava III. - Viera", "9"));
        ddlZbor.Items.Add(new ListItem("Bratislava City Church", "69"));
        ddlZbor.Items.Add(new ListItem("Brniště", "10"));
        ddlZbor.Items.Add(new ListItem("Brno", "11"));
        ddlZbor.Items.Add(new ListItem("Brno - neslyšící", "12"));
        ddlZbor.Items.Add(new ListItem("Broumov", "13"));
        ddlZbor.Items.Add(new ListItem("Cheb I.", "14"));
        ddlZbor.Items.Add(new ListItem("Cheb II.", "15"));
        ddlZbor.Items.Add(new ListItem("Cvikov", "16"));
        ddlZbor.Items.Add(new ListItem("České Budějovice", "17"));
        ddlZbor.Items.Add(new ListItem("Čjinové", "18"));
        ddlZbor.Items.Add(new ListItem("Děčín", "19"));
        ddlZbor.Items.Add(new ListItem("Hurbanovo", "20"));
        ddlZbor.Items.Add(new ListItem("Jablonec nad Nisou", "21"));
        ddlZbor.Items.Add(new ListItem("Jelšava", "22"));
        ddlZbor.Items.Add(new ListItem("Karlovy Vary", "23"));
        ddlZbor.Items.Add(new ListItem("Klenovec", "24"));
        ddlZbor.Items.Add(new ListItem("Komárno", "25"));
        ddlZbor.Items.Add(new ListItem("Košice", "26"));
        ddlZbor.Items.Add(new ListItem("Kraslice", "27"));
        ddlZbor.Items.Add(new ListItem("Kroměříž", "28"));
        ddlZbor.Items.Add(new ListItem("Kuřim", "29"));
        ddlZbor.Items.Add(new ListItem("Levice", "30"));
        ddlZbor.Items.Add(new ListItem("Liberec", "31"));
        ddlZbor.Items.Add(new ListItem("Liptovský Mikuláš", "32"));
        ddlZbor.Items.Add(new ListItem("Litoměřice", "33"));
        ddlZbor.Items.Add(new ListItem("Lovosice", "34"));
        ddlZbor.Items.Add(new ListItem("Lučenec", "35"));
        ddlZbor.Items.Add(new ListItem("Miloslavov", "36"));
        ddlZbor.Items.Add(new ListItem("Nesvady", "37"));
        ddlZbor.Items.Add(new ListItem("Nové Zámky - Radostná správa", "38"));
        ddlZbor.Items.Add(new ListItem("Olomouc", "39"));
        ddlZbor.Items.Add(new ListItem("Ostrava", "40"));
        ddlZbor.Items.Add(new ListItem("Panické Dravce", "41"));
        ddlZbor.Items.Add(new ListItem("Pardubice", "42"));
        ddlZbor.Items.Add(new ListItem("Plzeň", "43"));
        ddlZbor.Items.Add(new ListItem("Poprad", "44"));
        ddlZbor.Items.Add(new ListItem("Praha 3 - IBCP", "45"));
        ddlZbor.Items.Add(new ListItem("Praha 3 - Vinný kmen", "46"));
        ddlZbor.Items.Add(new ListItem("Praha 3 - Vinohrady", "47"));
        ddlZbor.Items.Add(new ListItem("Praha 4 - Na Topolce", "48"));
        ddlZbor.Items.Add(new ListItem("Praha 6 - ŠVCC", "49"));
        ddlZbor.Items.Add(new ListItem("Praha 13", "50"));
        ddlZbor.Items.Add(new ListItem("Příbor", "51"));
        ddlZbor.Items.Add(new ListItem("Revúcka Lehota", "52"));
        ddlZbor.Items.Add(new ListItem("Ružomberok", "53"));
        ddlZbor.Items.Add(new ListItem("Sokolov", "54"));
        ddlZbor.Items.Add(new ListItem("Srbsko", "71"));
        ddlZbor.Items.Add(new ListItem("Suchdol nad Odrou", "55"));
        ddlZbor.Items.Add(new ListItem("Štôla", "70"));
        ddlZbor.Items.Add(new ListItem("Šumperk", "56"));
        ddlZbor.Items.Add(new ListItem("Svatá Helena (RO)", "57"));
        ddlZbor.Items.Add(new ListItem("Svätý Peter", "58"));
        ddlZbor.Items.Add(new ListItem("Tekovské Lužany", "59"));
        ddlZbor.Items.Add(new ListItem("Teplice", "60"));
        ddlZbor.Items.Add(new ListItem("Teplá", "61"));
        ddlZbor.Items.Add(new ListItem("Uherské Hradiště", "62"));
        ddlZbor.Items.Add(new ListItem("Vavrišovo", "63"));
        ddlZbor.Items.Add(new ListItem("Vikýřovice", "64"));
        ddlZbor.Items.Add(new ListItem("Vsetín", "65"));
        ddlZbor.Items.Add(new ListItem("Vysoké Mýto", "66"));
        ddlZbor.Items.Add(new ListItem("Žatec", "67"));
        ddlZbor.Items.Add(new ListItem("Zlín", "68"));
        ddlZbor.Items.Add(new ListItem("Iný...", "-1"));
    }
}