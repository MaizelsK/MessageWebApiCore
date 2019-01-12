using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GmailService : IEmailService
    {
        private const string host = "smtp.gmail.com";
        private const int port = 587;
        private const string email = "domo.ddr@gmail.com";
        private const string password = "QazPlm3110weroiu";

        public async Task SendEmail(string to, string title, string body)
        {
            await Task.Run(() =>
            {
                MailMessage mailMessage = new MailMessage(email, to)
                {
                    Body = body,
                    Subject = title
                };

                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = host;
                    client.Port = port;
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(email, password);

                    client.SendAsync(mailMessage, null);
                }
            });
        }
    }
}
