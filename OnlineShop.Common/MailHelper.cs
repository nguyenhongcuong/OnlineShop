using NWebsec.Helpers;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace OnlineShop.Common
{
    public class MailHelper
    {
        public static bool SendMail(string toEmail , string subject , string content)
        {
            try
            {
                var host = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var port = ConfigurationManager.AppSettings["SMTPPort"].ToString();
                var fromEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                var password = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                var fromName = ConfigurationManager.AppSettings["FromName"].ToString();

                var smtpClient = new SmtpClient(host , Convert.ToInt32(port))
                {
                    UseDefaultCredentials = false ,
                    Credentials = new NetworkCredential(fromEmail , password) ,
                    DeliveryMethod = SmtpDeliveryMethod.Network ,
                    EnableSsl = true ,
                    Timeout = 10000
                };

                var mail = new MailMessage
                {
                    Body = content ,
                    Subject = subject ,
                    From = new MailAddress(fromEmail , fromName)
                };

                mail.To.Add(new MailAddress(toEmail));
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                smtpClient.Send(mail);
                return true;
            }
            catch (SmtpException e)
            {
                return false;
            }
        }
    }
}
