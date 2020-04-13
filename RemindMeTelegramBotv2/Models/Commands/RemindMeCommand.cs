using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Services;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Команда добавления напоминания
    /// </summary>
    public class RemindMeCommand : Command
    {
        public override string Name => "/addremind";

        private static Dictionary<int, CommandState> _commandStates;
        private readonly IRemindService _remindService;
        private readonly TelegramBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;
        private readonly RemindBuilder _remindBuilder;

        public RemindMeCommand(IRemindService remindService, TelegramBotClient botClient,
            IDbRepository<RemindEntity> dbRepository)
        {
            _commandStates = new Dictionary<int, CommandState>();
            _remindBuilder = new RemindBuilder();
            _remindService = remindService;
            _botClient = botClient;
            _dbRepository = dbRepository;
        }

        public override async Task ExecuteAsync(MessageDetails message)
        {
            IsComplete = false;
            var state = CommandState.Stage1;

            if (_commandStates.ContainsKey(message.FromId))
            {
                state = _commandStates[message.FromId];
            }

            if (message.MessageText == "/reset")
            {
                Reset(message);
                _remindBuilder.Reset();
            }

            //В зависимости от состояния, выбираем действие.
            switch (state)
            {
                case CommandState.Stage1:
                    await _botClient.SendTextMessageAsync(message.ChatId,
                        "О чём вам напомнить? (Сброс диалога, команда /reset)");
                    _remindBuilder.StartStage(message);
                    _commandStates.Add(message.FromId, CommandState.Stage2);
                    break;

                case CommandState.Stage2:
                    _remindBuilder.EnterTextStage(message);
                        await _botClient.SendTextMessageAsync(message.ChatId,
                            $"Когда напомнить? (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                    _commandStates[message.FromId] = CommandState.Stage3;
                    break;

                case CommandState.Stage3:
                    if (_remindBuilder.EnterDateStage(message))
                    {
                        var remind = _remindBuilder.GetRemindEntity();
                        _dbRepository.Create(remind);
                        await _botClient.SendTextMessageAsync(message.ChatId, "Создал напоминание");
                        _commandStates.Remove(message.FromId);
                        _remindService.TryAddToRemindsSequence(remind);
                        IsComplete = true;

                    }
                    else
                    {
                        await _botClient.SendTextMessageAsync(message.ChatId, $"Что-то Вы ввели не так...\n Напоминаю: (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //ToDo Сделать отдельную ресет команду
        private void Reset(MessageDetails message)
        {
            if (_commandStates.ContainsKey(message.FromId))
                    _commandStates.Remove(message.FromId);
            IsComplete = true;
        }
    }
}
