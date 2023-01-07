using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace UserRegExample.Models
{
    public class SendMail : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("mailtestemail2023@gmail.com");
                mailMessage.Subject = subject;
                mailMessage.Body = email + htmlMessage;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(email));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp-relay.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential();
                networkCredential.UserName = "mailtestemail2023@gmail.com";
                networkCredential.Password = "Lanet@123";
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 465;
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
