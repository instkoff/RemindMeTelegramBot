using System.Threading.Tasks;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class StartCommand : Command
    {
        private readonly TelegramBotClient _botClient;
        public override string Name { get; } = "/start";

        public StartCommand(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }
        public override async Task ExecuteAsync(MessageDetails message)
        {
            await _botClient.SendTextMessageAsync(message.FromId,
                "Приветствую, этот бот поможет тебе не забыть о важном\n" +
                "Комманды бота:\n" +
                "/send_keyboard - Клавиатура помощник, она же ниже :)\r\n" +
                "/addremind - Создать напоминание\r\n" +
                "/myremindslist - Список ваших напоминаний\r\n" +
                "/delremind - Удалить напоминание");
            await new SendKeyboardCommand(_botClient).ExecuteAsync(message);
            IsComplete = true;
        }
    }
}
