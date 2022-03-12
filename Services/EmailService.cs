using System.Net;
using System.Net.Mail;

namespace CadastroApi.Services;

public class EmailService
{
    public async void Send(
            string toEmail,
            string toName,
            string subject,
            string body,
            string fromEmail,
            string fromName
        )
    {
        var smtpConfig = Configuration.Smtp;

        using (var mail = new MailMessage())
        {
            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail, toName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using (var smtp = new SmtpClient(smtpConfig.Host, smtpConfig.Port))
            {
                smtp.Credentials = new NetworkCredential(smtpConfig.UserName, smtpConfig.Password);
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(mail);
            }
        }
    }
}

