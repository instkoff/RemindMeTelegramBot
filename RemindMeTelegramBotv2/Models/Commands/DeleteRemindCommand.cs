using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Команда удаления напоминания.
    /// </summary>
    public class DeleteRemindCommand : Command
    {
        public override string Name { get; } = "/delremind";

        private readonly TelegramBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;
        private readonly ICommandsCreator _commandsCreator;
        private static Dictionary<int, CommandState> _stateStorage;

        public DeleteRemindCommand(TelegramBotClient botClient, IDbRepository<RemindEntity> dbRepository, ICommandsCreator commandsCreator)
        {
            _botClient = botClient;
            _dbRepository = dbRepository;
            _commandsCreator = commandsCreator;
            _stateStorage = new Dictionary<int, CommandState>();
        }

        public override async Task ExecuteAsync(MessageDetails message)
        {
            IsComplete = false;
            var state = CommandState.Stage1;

            if (_stateStorage.ContainsKey(message.FromId))
            {
                state = _stateStorage[message.FromId];
            }

            switch (state)
            {
                case CommandState.Stage1:
                    await _botClient.SendTextMessageAsync(message.FromId, "Какое напоминание удалить? \n Введите номер:");
                    var command = _commandsCreator.CreateCommand("/myremindslist");
                    await command.ExecuteAsync(message);
                    _stateStorage.Add(message.FromId, CommandState.Stage2);
                    break;
                case CommandState.Stage2:
                    var remindsList = await _dbRepository.GetListAsync(r => r.TelegramUsernameId == message.FromId && r.State != RemindState.Completed);
                    if (int.TryParse(message.MessageText, out var index))
                    {
                        _dbRepository.Remove(remindsList[index - 1]);
                        await _botClient.SendTextMessageAsync(message.FromId, "Удалено.");
                        _stateStorage.Remove(message.FromId);
                        IsComplete = true;
                    }
                    else
                    {
                        await _botClient.SendTextMessageAsync(message.FromId, $"Что-то Вы ввели не так. Введите число от: 1 до {remindsList.Count}");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
