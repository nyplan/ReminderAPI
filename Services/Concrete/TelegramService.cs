using ReminderAPI.Entities;
using Telegram.Bot.Types;
using Telegram.Bot;
using ReminderAPI.Repositories.Abstract;

namespace ReminderAPI.Repositories.Concrete
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient _botClient;

        public TelegramService(string botToken)
        {
            _botClient = new TelegramBotClient(botToken);
        }

        public async Task SendReminderAsync(Reminder reminder)
        {
            ChatId chatId = new ChatId(reminder.Recipient);
            await _botClient.SendTextMessageAsync(chatId, reminder.Content);
        }
    }
}
