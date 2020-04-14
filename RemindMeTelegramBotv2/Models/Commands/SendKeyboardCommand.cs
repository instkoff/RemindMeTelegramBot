using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class SendKeyboardCommand : Command
    {
        /// <summary>
        /// Команда старта диалога с ботом, вывод меню
        /// </summary>
        public override string Name => "/send_keyboard";

        private readonly TelegramBotClient _botClient;

        public SendKeyboardCommand(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public override async Task ExecuteAsync(MessageDetails message)
        {
            IsComplete = false;

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                // first row
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Создать напоминание", "/addremind"),
                    InlineKeyboardButton.WithCallbackData("Удалить напоминание", "/delremind"),
                },
                // second row
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Вывести список моих напоминаний", "/myremindslist"),
                    //InlineKeyboardButton.WithCallbackData("2.2", "22"),
                },
            });
            await _botClient.SendTextMessageAsync(message.ChatId,"Клавиатура", replyMarkup: inlineKeyboard);
            IsComplete = true;
        }
    }
}
