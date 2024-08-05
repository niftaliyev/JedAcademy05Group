using System.Net;
using System.Net.Mail;

namespace EShop.Services;

public class BrevoEmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var smtpClient = new SmtpClient("smtp-relay.brevo.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("79a41a001@smtp-brevo.com", "shxtM0vd5zYLOmAR"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(email),
            Subject = subject,
            Body = message,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        smtpClient.Send(mailMessage);
    }
}
