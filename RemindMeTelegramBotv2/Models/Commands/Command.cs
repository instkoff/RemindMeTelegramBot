using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Scheduler.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public bool IsComplete;
        public abstract Task ExecuteAsync(TelegramBotClient botClient, MessageInfo message, IDbRepository<RemindEntity> remindRepository);

        public bool Contains(string command)
        {
            return command.Contains(this.Name) /*&& command.Contains(BotSettings.Name)*/;
        }
    }
}
