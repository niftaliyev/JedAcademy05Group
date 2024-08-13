using Microsoft.AspNetCore.Identity.UI.Services;

namespace ToDoList.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Implement your email sending logic here (e.g., using SMTP, SendGrid, etc.)
        return Task.CompletedTask;
    }
}
