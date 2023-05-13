using ReminderAPI.Entities;
using System.Linq.Expressions;

namespace ReminderAPI.Repositories.Abstract
{
    public interface IReminderRepository
    {
        Task CreateAsync(Reminder reminder);
        IQueryable<Reminder> Read();
        IQueryable<Reminder> Read(Expression<Func<Reminder, bool>> expression);
        Task<List<Reminder>> GetRemindersToSendAsync(DateTime currentTime);
        Task UpdateReminderAsync(Reminder reminder);
        Task DeleteRangeAsync(IEnumerable<Reminder> reminders);
    }
}
