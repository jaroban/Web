using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

public class Email
{
    private const string SmtpServer = "smtp.gmail.com";
    private const int SmtpPort = 587;
    private const string SenderAddress = "<your email>@gmail.com";
    private const string SenderName = "<sender name shown in email headers>";
    private const string Password = "<your email account password>";

    public int Id;
    public string Subject;
    public string Address;
    public string Body;
    public string Result;
    public bool Success;

    public Email(int id, string address, string subject, string body)
    {
        Id = id;
        Address = address;
        Subject = subject;
        Body = body;
    }

    public void Send()
    {
        Result = "";
        Success = true;
        try
        {
            // send email
            var senderAddress = new MailAddress(SenderAddress, SenderName);
            var toAddress = new MailAddress(Address);
            var smtp = new SmtpClient
            {
                Host = SmtpServer,
                Port = SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderAddress.Address, Password),
                Timeout = 5000
            };
            smtp.ServicePoint.MaxIdleTime = 2;
            smtp.ServicePoint.ConnectionLimit = 1;
            using (var mail = new MailMessage(senderAddress, toAddress))
            {
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                smtp.Send(mail);
            }
        }
        catch(Exception ex)
        {
            Result = ex.Message + " " + ex.InnerException;
            Success = false;
        }
    }
}