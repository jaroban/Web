using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mailer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press space to stop...");
            do
            {
                while (!Console.KeyAvailable)
                {
                    try
                    {
                        var emails = Database.GetEmails();
                        if(emails.Count > 0)
                        {
                            foreach (var email in emails)
                            {
                                Console.WriteLine("Sending email to {0}...", email.Address);
                                email.Send();
                                if (!email.Success)
                                {
                                    Console.WriteLine(email.Result);
                                }
                                Thread.Sleep(100);
                            }
                            Database.UpdateEmails(emails);
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message + " " + ex.InnerException);
                    }
                    Thread.Sleep(1000);
                }
            } 
            while (Console.ReadKey(true).Key != ConsoleKey.Spacebar);
        }
    }
}
