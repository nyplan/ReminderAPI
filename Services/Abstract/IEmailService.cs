namespace ReminderAPI.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string recipient, string body);
    }
}
