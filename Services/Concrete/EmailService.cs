using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ReminderAPI.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string recipient, string body)
        {
            MimeMessage message = new();
            message.From.Add(new MailboxAddress("Nurlan", "xhaha775@gmail.com"));
            message.To.Add(new MailboxAddress("", recipient));

            BodyBuilder bodyBuilder = new()
            {
                HtmlBody = body
            };
            message.Body = bodyBuilder.ToMessageBody();

            using SmtpClient client = new();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync("xhaha775@gmail.com", "lpkeuuywpypdafbd");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
