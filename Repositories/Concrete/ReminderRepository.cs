using Microsoft.EntityFrameworkCore;
using ReminderAPI.Contexts;
using ReminderAPI.Entities;
using ReminderAPI.Repositories.Abstract;
using System.Linq.Expressions;

namespace ReminderAPI.Repositories.Concrete
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly AppDbContext _context;
        public ReminderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Reminder reminder)
        {
            await _context.Reminders.AddAsync(reminder);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Reminder> Read()
        {
            return _context.Reminders;
        }

        public IQueryable<Reminder> Read(Expression<Func<Reminder, bool>> expression)
        {
            return _context.Reminders.Where(expression);
        }

        public async Task<List<Reminder>> GetRemindersToSendAsync(DateTime currentTime)
        {
            return await _context.Reminders
                .Where(r => r.SendAt <= currentTime && !r.Sent)
                .ToListAsync();
        }

        public async Task UpdateReminderAsync(Reminder reminder)
        {
            _context.Reminders.Update(reminder);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<Reminder> reminders)
        {
            _context.RemoveRange(reminders);
            await _context.SaveChangesAsync();
        }
        
    }
}
