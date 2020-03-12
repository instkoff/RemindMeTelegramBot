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
            base.isComplete = false;
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
                                {
                        // first row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Создать напоминание", "/addremind"),
                            InlineKeyboardButton.WithCallbackData("Удалить напоминание", "/delremind"),
                        },
                        // second row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Вывести список моих напоминаний", "/myremindslist"),
                            //InlineKeyboardButton.WithCallbackData("2.2", "22"),
                        }
                    });
            await botClient.SendTextMessageAsync(message.Chat.Id, "Клавиатура", replyMarkup: inlineKeyboard);
            base.isComplete = true;
        }
    }
}
