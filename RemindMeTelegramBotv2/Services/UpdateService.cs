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
        private readonly ILogger<UpdateService> _logger;

        public UpdateService(IBotClient botClient, ILogger<UpdateService> logger)
        {
            _botClient = botClient;
            _logger = logger;
        }
        public async Task AnswerAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
                return;

            var message = update.Message;
            _logger.LogInformation("Received Message from {0}", message.Chat.Id);

            if (message.Type == MessageType.Text)
            {
                await _botClient.Client.SendTextMessageAsync(message.Chat.Id,"Привет!");
            }
        }
    }
}
