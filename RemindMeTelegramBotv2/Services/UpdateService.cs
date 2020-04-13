using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.Models;
using RemindMeTelegramBotv2.Models.Commands;

namespace RemindMeTelegramBotv2.Services
{
    /// <summary>
    /// Сервис получения обновлений от бота.
    /// </summary>
    public class UpdateService : IUpdateService
    {
        private readonly IBotClient _botClient;
        private readonly ICommandsCreator _commandsCreator;

        private static readonly Dictionary<int,Command> _fixedCommands = new Dictionary<int, Command>();


        public UpdateService(IBotClient botClient, ICommandsCreator commandsCreator)
        {
            _botClient = botClient;
            _commandsCreator = commandsCreator;
        }

        public async Task AnswerOn(MessageDetails messageDetails)
        {
            if (messageDetails == null)
                return;

            var command = _botClient.Commands.Contains(messageDetails.MessageText) ? _commandsCreator.CreateCommand(messageDetails.MessageText) : null;

            if (command != null)
            {
                if (_fixedCommands.ContainsKey(messageDetails.FromId))
                {
                    if (!_fixedCommands[messageDetails.FromId].IsComplete)
                    {
                        await _botClient.Client.SendTextMessageAsync(messageDetails.ChatId,
                            "Вы повторно ввели команду, закончите или сбросьте текущую");
                        return;
                    }
                    if (_fixedCommands[messageDetails.FromId].IsComplete)
                        _fixedCommands.Remove(messageDetails.FromId);
                }

                _fixedCommands.Add(messageDetails.FromId, command);
                await command.ExecuteAsync(messageDetails);
                return;
            }

            if (_fixedCommands.ContainsKey(messageDetails.FromId))
            {
                if (!_fixedCommands[messageDetails.FromId].IsComplete)
                {
                    await _fixedCommands[messageDetails.FromId].ExecuteAsync(messageDetails);
                }
                if (_fixedCommands[messageDetails.FromId].IsComplete)
                    _fixedCommands.Remove(messageDetails.FromId);
            }
        }
    }
}
