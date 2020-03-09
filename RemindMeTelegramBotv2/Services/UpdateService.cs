using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RemindMeTelegramBotv2.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RemindMeTelegramBotv2.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotClient _botClient;

        public UpdateService(IBotClient botClient)
        {
            _botClient = botClient;
        }
        public async Task AnswerAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
                return;

            foreach (var command in _botClient.Commands)
            {
                await command.ExecuteAsync(_botClient.Client, update.Message);
            }
        }
    }
}
