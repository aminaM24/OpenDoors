using System.Net.Mail;
using System.Net;

namespace OpenDoors.Helper
{
    public class EmailHelper
    {
        public static void PosaljiEmail(string to, string porukaSubject, string poruka)
        {
            String SendMailFrom = "opendoors263@gmail.com";
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage email = new MailMessage();
            // START
            email.From = new MailAddress(SendMailFrom);
            email.To.Add(to);
            email.CC.Add(SendMailFrom);
            email.Subject = porukaSubject;
            email.Body = poruka;
            //END
            SmtpServer.Timeout = 5000;
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "jhrmbwosbndmpnze");
            SmtpServer.Send(email);
        }
    }
}
