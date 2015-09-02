using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

public static class Emails
{
    public const string RegistrationSubject = 
        "Registrácia na kemp mládeže";

    public const string MailHeader =
        "<b>Celoslovenský kemp mládeže:<br/>";

    public const string MailFooter =
        "<br/>Všeobecne informácie:<br/>";
        
    public static string GetParticipantInfo(RegistrationEntry data)
    {
        return
            string.Format("<br/>Milá/milý {0} {1},<br/>", data.Meno, data.Priezvisko) +
            "vítame Ťa medzi prihlásenými účastníkmi kempu mládeže.<br/>";
    }

    public static string GetPaymentInfo(string ucelPlatby, string toPay)
    {
        return "<br/>" + 
            "Suma: " + toPay + "<br/>" +
            "Číslo účtu: SKxxxxxxxxxxxxxxx<br/>" +
            "(Starý formát: xxxxxxxxxxxxxxxx)<br/>" +
            "Variabilný symbol: <b>{0}</b><br/>" +
            "Účel platby: " + ucelPlatby;
    }

    public static string LivingConditions(int totalPeople)
    {
        return "<br/>" +
            "Nezabudni si doniesť spacák a pre istotu aj karimatku.<br/>" +
            ((totalPeople > 50) ? 
            "Počet ľudí zaregistrovaných na kemp presiahol kapacitu chaty (50 ľudí), " + 
            "preto si budeš musieť zabezpečiť stan, alebo spať pod širákom :)<br/>" : "");
    }

    public static string GetSingle(RegistrationEntry data, string toPay, int totalPeople)
    {
        var registrationFee = Common.FormatMoney(data.RegistrationFee);
        return
            MailHeader +
            GetParticipantInfo(data) +
            "<br/>Prosím pošli sumu <b>" + toPay + "</b> bankovým prevodom:<br/>" +
            GetPaymentInfo(string.Format("{0} {1} (uveď bez diakritiky)", data.Meno, data.Priezvisko), toPay) +
            LivingConditions(totalPeople) +
            MailFooter;
    }

    public static string GetMultiple(RegistrationEntry data, string payerEmail, int totalPeople)
    {
        return
            MailHeader +
            GetParticipantInfo(data) +
            string.Format("<br/><b>Na konferenciu Ťa zaregistroval(a) a zaplatil(a) za teba {0}.</b><br/>", payerEmail) +
            LivingConditions(totalPeople) +
            MailFooter;
    }

    public static string GetMultiplePayerRegistered(List<RegistrationEntry> data, int index, string toPay, int totalPeople)
    {
        return
            MailHeader +
            GetParticipantInfo(data[index]) +
            "<br/>Tvoja celková čiastka za konferenciu je <b>v hodnote " + toPay + "</b>, táto čiastka obsahuje aj všetky registračné poplatky " +
            "mládežníkov, ktorých si zaregistroval(a) (" + string.Join(", ", data.Select(x => x.Meno + " " + x.Priezvisko)) + ").<br/>" +
            GetPaymentInfo(string.Format("{0} {1} (uveď bez diakritiky)", data[index].Meno, data[index].Priezvisko), toPay) +
            LivingConditions(totalPeople) +
            MailFooter;
    }

    public static string GetMultiplePayerNotRegistered(List<RegistrationEntry> data, string toPay, string payerEmail)
    {
        return
            MailHeader +
            "<br/>Ďakujeme za Vašu registráciu.<br/>" +
            "<br/>Zaregistrovali ste:<br/>" +
            string.Join("<br/>", data.Select(x => x.Meno + " " + x.Priezvisko)) + "<br/>" +
            "<br/>Celková suma k úhrade je <b>" + toPay + "</b><br/>" +
            GetPaymentInfo(payerEmail, toPay) +
            MailFooter;
    }
}