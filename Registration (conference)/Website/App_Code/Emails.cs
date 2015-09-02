using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

public static class Emails
{
    public const string RegistrationSubject = 
        "Registrácia na Mládežnícku konferenciu 2015";

    public const string MailHeader = 
        "<b>Mládežnícka konferencia 2015</b><br/>" +
        "13. - 15. február 2015<br/>";

    public const string SportRegistration =
        "<br/>Registrácia volejbalového alebo futbalového teamu je v samostatnej prihláške";

    public const string MailFooter1 =
        "<br/><b>Informácie ohľadom registrácie:</b> xxxxxxxxx@gmail.com<br/>";
        
    public const string MailFooter2 = "<br/>Na požehnaný čas aj s tebou sa teší";
    public const string MailFooter3 = "<br/>Organizačný team MK2015";

    public static string GetParticipantInfo(RegistrationEntry data)
    {
        var ubytovanie = GetUbytovanie(data);
        var strava = GetStrava(data);
        return
            string.Format("<br/>Milá/milý {0} {1},<br/>", data.Meno, data.Priezvisko) +
            "vítame Ťa medzi prihlásenými účastníkmi Mládežníckej konferencie.<br/>" +
            "<br/><b>Máš objednané:</b><br/>" +
            (ubytovanie.Count > 0 ? "<b>Ubytovanie:</b> " + string.Join(", ", ubytovanie) + " - k ubytovaniu je potrebný vlastný spacák a karimatka<br/>" : "") +
            (strava.Count > 0 ? "<b>Strava:</b> " + string.Join(", ", strava) + "<br/>" : "") +
            (data.IdTricko > 0 ? "<b>Tričko:</b> " + data.Tricko + "<br/>" : "") +
            (data.Sluziaci ? "V registračnom formulári si zaškrtol/zaškrtla, že si ochotný/ochotná slúžiť a byť k dispozícii pre rôzne úlohy počas celého " +
                             "času trvania konferencie. Ohľadom možnosti služby Ťa budeme kontaktovať.<br/>" : "");
    }

    public static string GetPaymentInfo(string ucelPlatby, string toPay, int idCurrency)
    {
        return "<br/>" + 
            (idCurrency == 1 ?
            "Suma: " + toPay + "<br/>" +
            "Číslo účtu: xxxxxxxxxxxxxxxxxxxxx<br/>" +
            "(Starý formát: xxxxxxxxxxxxxxxxxxx)<br/>" +
            "Variabilný symbol: <b>{0}</b><br/>" +
            "Účel platby: " + ucelPlatby
            :
            "Suma: " + toPay + "<br/>" +
            "Číslo účtu: xxxxxxxxxxxxxxxxx<br/>" +
            "Variabilný symbol: <b>xxxxxxxxxx</b><br/>" +
            "Špecifický symbol: <b>{0}</b><br/>" +
            "Účel platby: " + ucelPlatby) +
            "<br/>";
    }

    public static List<string> GetUbytovanie(RegistrationEntry data)
    {
        var ubytovanie = new List<string>();
        if (data.UbytovaniePiatokSobota) ubytovanie.Add("piatok-sobota" + (data.TichaTriedaPiatokSobota ? " (tichá trieda)" : ""));
        if (data.UbytovanieSobotaNedela) ubytovanie.Add("sobota-nedeľa" + (data.TichaTriedaSobotaNedela ? " (tichá trieda)" : ""));
        return ubytovanie;
    }

    public static List<string> GetStrava(RegistrationEntry data)
    {
        var strava = new List<string>();
        if (data.PiatokVecera) strava.Add("piatok večera");
        if (data.PiatokVecera2) strava.Add("piatok druhá večera");
        if (data.SobotaRanajky) strava.Add("sobota raňajky");
        if (data.SobotaObed) strava.Add("sobota obed");
        if (data.SobotaVecera) strava.Add("sobota večera");
        if (data.SobotaVecera2) strava.Add("sobota druhá večera");
        if (data.NedelaRanajky) strava.Add("nedeľa raňajky");
        if (data.NedelaObed) strava.Add("nedeľa obed");
        return strava;
    }

    public static string GetSingle(RegistrationEntry data, string toPay)
    {
        var registrationFee = Common.FormatMoney(data.RegistrationFee, data.IdCurrency);
        return
            MailHeader +
            GetParticipantInfo(data) +
            "<br/>Tvoja celková čiastka za konferenciu je <b>v hodnote " + toPay + "</b>, táto čiastka obsahuje konferenčný poplatok (" + registrationFee + ") " +
            "a pripočítaný je aj tvoj sponzorský príspevok, ak si ho zvolil(a).<br/>" +
            GetPaymentInfo(string.Format("{0} {1} (uveď bez diakritiky)", data.Meno, data.Priezvisko), toPay, data.IdCurrency) +
            SportRegistration +
            MailFooter1 +
            MailFooter2 +
            MailFooter3;
    }

    public static string GetMultiple(RegistrationEntry data, string payerEmail)
    {
        var ubytovanie = GetUbytovanie(data);
        var strava = GetStrava(data);
        return
            MailHeader +
            GetParticipantInfo(data) +
            string.Format("<br/><b>Na konferenciu Ťa zaregistroval(a) a zaplatil(a) za teba {0}.</b><br/>", payerEmail) +            
            SportRegistration +
            MailFooter1 +
            MailFooter2 +
            MailFooter3;
    }

    public static string GetMultiplePayerRegistered(List<RegistrationEntry> data, int index, string toPay)
    {
        return
            MailHeader +
            GetParticipantInfo(data[index]) +
            "<br/>Tvoja celková čiastka za konferenciu je <b>v hodnote " + toPay + "</b>, táto čiastka obsahuje aj všetky registračné poplatky " +
            "mládežníkov, ktorých si zaregistroval(a) (" + string.Join(", ", data.Select(x => x.Meno + " " + x.Priezvisko)) + ").<br/>" +
            GetPaymentInfo(string.Format("{0} {1} (uveď bez diakritiky)", data[index].Meno, data[index].Priezvisko), toPay, data[index].IdCurrency) +
            SportRegistration +
            MailFooter1 +
            MailFooter2 +
            MailFooter3;
    }

    public static string GetMultiplePayerNotRegistered(List<RegistrationEntry> data, string toPay, string payerEmail)
    {
        return
            MailHeader +
            "<br/>Ďakujeme za Vašu registráciu.<br/>" +
            "<br/>Zaregistrovali ste:<br/>" +
            string.Join("<br/>", data.Select(x => x.Meno + " " + x.Priezvisko)) + "<br/>" +
            "<br/>Celková suma k úhrade je <b>" + toPay + "</b><br/>" +
            GetPaymentInfo(payerEmail, toPay, data[0].IdCurrency) +
            MailFooter1 +
            MailFooter3;
    }
}