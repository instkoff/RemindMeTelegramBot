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

        private static readonly Dictionary<int, Command> FixedCommands = new Dictionary<int, Command>();


        public UpdateService(IBotClient botClient, ICommandsCreator commandsCreator)
        {
            _botClient = botClient;
            _commandsCreator = commandsCreator;
        }

        public async Task AnswerOn(MessageDetails messageDetails)
        {
            if (messageDetails == null)
                return;

            if (FixedCommands.ContainsKey(messageDetails.FromId))
            {
                if (!FixedCommands[messageDetails.FromId].IsComplete)
                {
                    await FixedCommands[messageDetails.FromId].ExecuteAsync(messageDetails);
                }

                if (FixedCommands[messageDetails.FromId].IsComplete)
                    FixedCommands.Remove(messageDetails.FromId);


            }
            else
            {
                var command = _botClient.Commands.Contains(messageDetails.MessageText) ? _commandsCreator.CreateCommand(messageDetails.MessageText) : null;
                if (command != null)
                {
                    FixedCommands.Add(messageDetails.FromId, command);
                    await command.ExecuteAsync(messageDetails);

                    if (FixedCommands[messageDetails.FromId].IsComplete)
                        FixedCommands.Remove(messageDetails.FromId);

                }
            }

        }
    }
}
