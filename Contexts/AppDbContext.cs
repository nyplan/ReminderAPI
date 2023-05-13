using Microsoft.EntityFrameworkCore;
using ReminderAPI.Entities;

namespace ReminderAPI.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Reminder> Reminders { get; set; }
    }
}
