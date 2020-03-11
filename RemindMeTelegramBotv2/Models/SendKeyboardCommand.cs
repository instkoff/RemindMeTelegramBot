using System;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace RemindMeTelegramBotv2.Models
{
    public class SendKeyboardCommand : Command
    {
        public override string Name => "/start";

        public override async Task ExecuteAsync(TelegramBotClient botClient, Message message, IDbRepository<RemindEntity> repository)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                {
                        // first row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Создать напоминание", "/addremind"),
                            InlineKeyboardButton.WithCallbackData("1.2", "12"),
                        },
                        // second row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("2.1", "21"),
                            InlineKeyboardButton.WithCallbackData("2.2", "22"),
                        }
                    });
            await botClient.SendTextMessageAsync(message.Chat.Id, "Клавиатура", replyMarkup: inlineKeyboard);
        }
    }
}
