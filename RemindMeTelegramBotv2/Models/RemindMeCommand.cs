using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindMeCommand
    {
        public string Name { get; } = "Напомнить";

        public async Task<RemindEntity> ExecuteAsync(TelegramBotClient botClient, Message message, int userStage)
        {
            var remind = new RemindEntity();
            switch (userStage)
            {
                case 1:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "О чём вам напомнить?");
                    break;
                case 2:
                    remind.RemindText = message.Text;
                    break;
                case 3:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Когда напомнить?");
                    break;
                case 4:
                    return remind
            }

        }
    }
}
