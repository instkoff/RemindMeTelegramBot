using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindMeCommand
    {
        public string Name { get; } = "Напомнить";
        private RemindEntity _remind;

        public RemindEntity Remind
        {
            get => _remind;
            private set => _remind = value;
        }

        public async Task<RemindEntity> ExecuteAsync(TelegramBotClient botClient, Message message, UserEntity user)
        {
            switch (user.Stage)
            {
                case 1:
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"О чём вам напомнить {user.Name}?");
                    break;
                case 2:
                    _remind.RemindText = message.Text;
                    return _remind;
                case 3:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Когда напомнить?");
                    break;
                case 4:
                    _remind.AlarmTime = DateTime.ParseExact(message.Text, "dd:MM:yyyy HH:mm",CultureInfo.InvariantCulture);
                    _remind.CurrentTime = DateTime.Now;
                    return _remind;
            }

            return _remind;
        }
    }
}
