using ReminderAPI.Entities;

namespace ReminderAPI.Repositories.Abstract
{
    public interface ITelegramService
    {
        Task SendReminderAsync(Reminder reminder);
    }
}
