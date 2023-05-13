using ReminderAPI.Repositories.Abstract;
using ReminderAPI.Services.EmailService;

namespace ReminderAPI.Services.Concrete
{
    public class SendReminderService : BackgroundService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly ITelegramService _reminderSender;
        private readonly IEmailService _emailService;

        public SendReminderService(IReminderRepository reminderRepository, ITelegramService reminderSender, IEmailService emailService)
        {
            _reminderRepository = reminderRepository;
            _reminderSender = reminderSender;
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Get reminders from the database that have a sendAt time in the past
                var reminders = await _reminderRepository.GetRemindersToSendAsync(DateTime.UtcNow);

                foreach (var reminder in reminders)
                {
                    if (reminder.Method == "telegram")
                    {
                        // Send the reminder
                        await _reminderSender.SendReminderAsync(reminder);

                        // Mark the reminder as sent in the database
                        reminder.Sent = true;
                        await _reminderRepository.UpdateReminderAsync(reminder);
                    }
                    else
                    {
                        // Send the reminder
                        await _emailService.SendEmailAsync(reminder.Recipient, reminder.Content);

                        // Mark the reminder as sent in the database
                        reminder.Sent = true;
                        await _reminderRepository.UpdateReminderAsync(reminder);
                    }
                }

                // Delay the execution to check for reminders periodically
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
