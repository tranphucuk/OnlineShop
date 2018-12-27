using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common
{
    public class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                var host = ConfigHelper.GetValueByKey("SMTPHost");
                var port = int.Parse(ConfigHelper.GetValueByKey("SMTPPort"));
                var fromEmail = ConfigHelper.GetValueByKey("FromEmailAddress");
                var password = ConfigHelper.GetValueByKey("FromEmailPassword");
                var fromName = ConfigHelper.GetValueByKey("FromName");

                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Timeout = 100000
                };

                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };

                var listMailAddress = toEmail.Split(',');
                foreach (var item in listMailAddress)
                {
                    mail.To.Add(new MailAddress(item));
                }

                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);

                return true;
            }
            catch (SmtpException ex)
            {
                return false;
            }
        }
    }
}
