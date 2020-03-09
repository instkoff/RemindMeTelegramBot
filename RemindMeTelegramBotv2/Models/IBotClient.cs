using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    public interface IBotClient
    {
        TelegramBotClient Client { get; }
        IReadOnlyList<ICommand> Commands { get; }
    }
}
