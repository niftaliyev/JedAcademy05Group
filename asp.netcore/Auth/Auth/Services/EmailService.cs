using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace Auth.Services;

public class EmailService
{
    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Auth test Admin", "postmaster@sandboxf03e7aa1d5c643a9b10ff8e96d875b91.mailgun.org"));
        emailMessage.To.Add(new MailboxAddress("Auth user login", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.mailgun.org", 587,false);
            await client.AuthenticateAsync("postmaster@sandboxf03e7aa1d5c643a9b10ff8e96d875b91.mailgun.org", "cb33da5d99b34acd1f2a7a2fcdd4d43c-0f1db83d-9a02cbe3");
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
