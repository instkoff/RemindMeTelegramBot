using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public interface ICommand
    {
        string Name { get; }
        Task ExecuteAsync(TelegramBotClient botClient, Message message);
    }
}
