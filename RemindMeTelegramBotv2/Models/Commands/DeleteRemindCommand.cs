using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class DeleteRemindCommand : Command
    {
        public override string Name { get; } = "/delete_remind";
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;
        private readonly ICommandsCreator _commandsCreator;
        private static Dictionary<int, States> _stateStorage;

        public DeleteRemindCommand(IBotClient botClient, IDbRepository<RemindEntity> dbRepository, ICommandsCreator commandsCreator)
        {
            _botClient = botClient;
            _dbRepository = dbRepository;
            _commandsCreator = commandsCreator;
            _stateStorage = new Dictionary<int, States>();
        }

        public override async Task ExecuteAsync(MessageDetails message)
        {
            IsComplete = false;
            var botClient = _botClient.Client;
            var state = States.Stage1;

            if (_stateStorage.ContainsKey(message.FromId))
            {
                state = _stateStorage[message.FromId];
            }

            switch (state)
            {
                case States.Stage1:
                    await _botClient.Client.SendTextMessageAsync(message.FromId, "Какое напоминание удалить? \n Введите номер:");
                    var command = _commandsCreator.CreateCommand("/myremindslist");
                    await command.ExecuteAsync(message);
                    break;
                case States.Stage2:
                    break;
                case States.Stage3:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
