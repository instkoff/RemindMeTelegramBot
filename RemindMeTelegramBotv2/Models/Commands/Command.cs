using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public bool IsComplete;
        public abstract Task ExecuteAsync(MessageDetails message);

        public bool Contains(string command)
        {
            return command.Contains(this.Name) /*&& command.Contains(BotSettings.Name)*/;
        }
    }
}
