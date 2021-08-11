using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.Service.Email
{
    public class SendMail : ISendMail
    {
        public async Task<bool> SendMailMethod(string email, string content, string subject)
        {

            try
            {
                MimeMessage message = new MimeMessage();
                MailboxAddress from = new MailboxAddress("NoName", "NoName@gmail.com");
                message.From.Add(from);
                MailboxAddress to = new MailboxAddress(email, email);
                message.To.Add(to);
                message.Subject = subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = content;
                message.Body = bodyBuilder.ToMessageBody();
                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("test19091999@gmail.com", "201792298");
                    await client.SendAsync(message);
                    client.Disconnect(true);
                    client.Dispose();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
