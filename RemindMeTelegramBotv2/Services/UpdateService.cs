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
            switch (update.Type)
            {
                case UpdateType.Message:
                    foreach (var command in _botClient.Commands)
                    {
                        if (command.Contains(update.Message.Text))
                            await command.ExecuteAsync(_botClient.Client, update.Message);
                    }
                    break;
                case UpdateType.CallbackQuery:
                    foreach (var command in _botClient.Commands)
                    {
                        if (command.Contains(update.CallbackQuery.Data))
                            await command.ExecuteAsync(_botClient.Client, update.CallbackQuery.Message);
                    }
                    break;
                default:
                    await _botClient.Client.SendTextMessageAsync(update.Message.Chat.Id, "Команда не распознана", replyToMessageId: update.Message.MessageId);
                    break;
            }
        }
    }
}
