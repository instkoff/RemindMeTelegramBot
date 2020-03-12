using System;
using System.Linq;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RemindMeTelegramBotv2.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _remindRepository;

        private static Command fixedCommand;

        public UpdateService(IBotClient botClient, IDbRepository<RemindEntity> remindRepository)
        {
            _remindRepository = remindRepository;
            _botClient = botClient;
        }
        public async Task AnswerOnMessageAsync(Update update)
        {
            if (update == null)
                return;

            var message = update.Message;

            await CommandProcessorAsync(message);
        }
        public async Task AnswerOnCallbackQueryAsync(Update update)
        {
            if (update == null)
                return;
        }

        private async Task CommandProcessorAsync(Message message)
        {
            if (fixedCommand != null)
            {
                if (fixedCommand.isComplete == false)
                    await fixedCommand.ExecuteAsync(_botClient.Client, message, _remindRepository);
                if (fixedCommand.isComplete == true)
                {
                    await _botClient.Commands.SingleOrDefault(c => c.Name == "/start")
                        ?.ExecuteAsync(_botClient.Client, message, _remindRepository);
                    fixedCommand = null;
                    return;
                }
            }

            if (fixedCommand == null)
            {
                foreach (var command in _botClient.Commands)
                {
                    if (command.Contains(message.Text))
                    {
                        fixedCommand = command;
                        await command.ExecuteAsync(_botClient.Client, message, _remindRepository);
                    }
                }
            }
        }

    }
}
