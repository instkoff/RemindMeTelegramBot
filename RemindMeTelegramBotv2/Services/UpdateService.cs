using System.Threading.Tasks;
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

            var message = update.Message;

            if (message.Type == MessageType.Text)
            {
                await _botClient.Client.SendTextMessageAsync(message.Chat.Id,"Привет!");
            }
        }
    }
}
