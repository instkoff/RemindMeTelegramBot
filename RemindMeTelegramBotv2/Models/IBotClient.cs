using System.Collections.Generic;
using RemindMeTelegramBotv2.Scheduler.Commands;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    public interface IBotClient
    {
        TelegramBotClient Client { get; }
        IReadOnlyList<Command> Commands { get; }
    }
}
