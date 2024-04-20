using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Email
{

    public class EmailSender : IEmailSender
    {

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                const string fromEmail = "info@techsitekw.com";
                string fromPassword = "Tgyf$8861";
                var message = new MailMessage
                {
                    From = new MailAddress(fromEmail),
                    To = { email },
                    Subject = subject,

                    Body = $"<html><body>{htmlMessage}</body></html>",
                IsBodyHtml = true,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                };
                using (SmtpClient smtpClient = new SmtpClient("webmail.techsitekw.com"))
                {
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtpClient.Port = 25;
                    smtpClient.EnableSsl = false;
                    smtpClient.Send(message);
                }
            }
            //try
            //{
            //    string fromMail = "info@techsitekw.com";
            //    //string fromPassword = "Tgyf$8861";

            //    MailMessage message = new MailMessage();
            //    message.From = new MailAddress(fromMail);
            //    message.Subject = subject;
            //    message.Body = $"<html><body>{htmlMessage}</body></html>";
            //    message.IsBodyHtml = true;
            //    message.To.Add(email);

            //    using (var smtpClient = new SmtpClient("smtp.techsitekw.com"))
            //    {
            //        smtpClient.Port = 465;
            //        smtpClient.Credentials = new NetworkCredential(fromMail, fromPassword);
            //        smtpClient.EnableSsl = true;

            //        await smtpClient.SendMailAsync(message);
            //    }
            //}
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; 
            }
        }

        public Task SendEmailAsync(Message message)
        {
            throw new System.NotImplementedException();
        }
    }
}
