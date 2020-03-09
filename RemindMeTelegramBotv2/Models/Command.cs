using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract Task ExecuteAsync(TelegramBotClient botClient, Message message);

        public bool Contains(string command)
        {
            return command.Contains(this.Name) /*&& command.Contains(BotSettings.Name)*/;
        }
    }
}
