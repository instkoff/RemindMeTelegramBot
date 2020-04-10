using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Services;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class RemindMeCommand : Command
    {
        public override string Name => "/addremind";

        private static Dictionary<int, RemindEntity> _remindEntities;
        private readonly IRemindService _remindService;
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;

        public RemindMeCommand(IRemindService remindService, IBotClient botClient,
            IDbRepository<RemindEntity> dbRepository)
        {
            _remindEntities = new Dictionary<int, RemindEntity>();
            _remindService = remindService;
            _botClient = botClient;
            _dbRepository = dbRepository;
        }

        public override async Task ExecuteAsync(MessageDetails message)
        {
            IsComplete = false;
            var botClient = _botClient.Client;
            var state = RemindState.Start;

            if (_remindEntities.ContainsKey(message.FromId))
            {
                state = _remindEntities[message.FromId].State;
            }

            if (await Reset(message)) return;

            switch (state)
            {
                case RemindState.Start:
                    await botClient.SendTextMessageAsync(message.ChatId,
                        "О чём вам напомнить? (Сброс диалога, команда /reset)");
                    _remindEntities.Add(message.FromId, StartStage(message));
                    break;

                case RemindState.EnterText:
                    if (_remindEntities.ContainsKey(message.FromId))
                    {
                        _remindEntities[message.FromId] = EnterTextStage(message);
                        await botClient.SendTextMessageAsync(message.ChatId,
                            $"Когда напомнить? (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                    }
                    break;

                case RemindState.EnterDate:
                    if (_remindEntities.ContainsKey(message.FromId))
                    {
                        var remind = EnterDateStage(message);
                        if (remind == null)
                        {
                            await botClient.SendTextMessageAsync(message.ChatId, $"Что-то Вы ввели не так...\n Напоминаю: (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                        }
                        else
                        {
                            _dbRepository.Create(remind);
                            await botClient.SendTextMessageAsync(message.ChatId, "Создал напоминание");
                            _remindEntities.Remove(message.FromId);
                            _remindService.TryAddToRemindsSequence(remind);
                            IsComplete = true;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static RemindEntity EnterDateStage(MessageDetails message)
        {
            string[] formats =
            {
                "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd:MM:yyyy HH:mm", "dd.MM.yyyy HH:mm",
                "dd/MM/yy HH:mm"
            };

            if (DateTime.TryParseExact(message.MessageText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var outDate))
            {
                var newRemindEntity = new RemindEntity(message.FromId, message.Username, message.ChatId)
                {
                    RemindText = _remindEntities[message.FromId].RemindText,
                    State = RemindState.Created,
                    EndTime = outDate.ToUniversalTime()
                };
                return newRemindEntity;
            }

            return null;
        }

        //ToDo Сделать ресет в UpdateService
        private async Task<bool> Reset(MessageDetails message)
        {
            if (message.MessageText == "/reset")
            {
                await _botClient.Client.SendTextMessageAsync(message.ChatId, "Отмена...");
                if (_remindEntities[message.FromId] != null)
                    _remindEntities.Remove(message.FromId);
                IsComplete = true;
                return true;
            }

            return false;
        }

        private static RemindEntity EnterTextStage(MessageDetails message)
        {
            var newRemindEntity = new RemindEntity(message.FromId, message.Username, message.ChatId)
            {
                RemindText = message.MessageText,
                State = RemindState.EnterDate
            };
            return newRemindEntity;
        }


        private static RemindEntity StartStage(MessageDetails message)
        {
            return new RemindEntity(message.FromId, message.Username, message.ChatId)
            {
                State = RemindState.EnterText
            };
        }
    }
}
