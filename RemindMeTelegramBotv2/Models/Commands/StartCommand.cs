using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class StartCommand : Command
    {
        public override string Name => "/start";

        private readonly IBotClient _botClient;

        public StartCommand(IBotClient botClient)
        {
            _botClient = botClient;
        }

        public override async Task ExecuteAsync(MessageDetails message)
        {
            IsComplete = false;
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
            await _botClient.Client.SendTextMessageAsync(message.ChatId,"Клавиатура", replyMarkup: inlineKeyboard);
            IsComplete = true;
        }
    }
}
