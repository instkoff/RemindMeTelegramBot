using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public bool isComplete;
        public abstract Task ExecuteAsync(TelegramBotClient botClient, Message message, IDbRepository<RemindEntity> remindRepository);

        public bool Contains(string command)
        {
            return command.Contains(this.Name) /*&& command.Contains(BotSettings.Name)*/;
        }
    }
}
