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

        public RemindMeCommand(IRemindService remindService)
        {
            _remindService = remindService;
            _remindEntities = new Dictionary<int, RemindEntity>();
        }

        public override async Task ExecuteAsync(TelegramBotClient botClient, MessageInfo message,
            IDbRepository<RemindEntity> remindRepository)
        {
            IsComplete = false;
            RemindEntity remindEntity = null;
            RemindEntity.State state = RemindEntity.State.Start;

            if (_remindEntities.ContainsKey(message.FromId))
            {
                remindEntity = _remindEntities[message.FromId];
                state = remindEntity.GetState();
            }


            if (message.MessageText == "/reset")
            {
                await botClient.SendTextMessageAsync(message.ChatId, "Отмена...");
                if (remindEntity != null)
                    _remindEntities.Remove(message.FromId);
                IsComplete = true;
                return;
            }

            switch (state)
            {
                case RemindEntity.State.Start:
                    var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local);
                    var newRemind = new RemindEntity(message.FromId, message.Username, message.ChatId)
                    {
                        StartTime = now,
                        RemindText = ""
                    };
                    await botClient.SendTextMessageAsync(message.ChatId, "О чём вам напомнить? (Сброс диалога, команда /reset)");
                    newRemind.SetState(RemindEntity.State.EnterText);
                    _remindEntities.Add(message.FromId,newRemind);
                    break;
                case RemindEntity.State.EnterText:
                    if (remindEntity != null)
                    {
                        remindEntity.RemindText = message.MessageText;
                        await botClient.SendTextMessageAsync(message.ChatId,
                            $"Когда напомнить? (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                        remindEntity.SetState(RemindEntity.State.EnterDate);
                        _remindEntities[message.FromId] = remindEntity;
                    }

                    break;
                case RemindEntity.State.EnterDate:
                    string[] formats = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd:MM:yyyy HH:mm", "dd.MM.yyyy HH:mm",
                        "dd/MM/yy HH:mm" };
                    if (DateTime.TryParseExact(message.MessageText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var outDate))
                    {
                        if (remindEntity != null)
                        {
                            var endTime = TimeZoneInfo.ConvertTime(outDate, TimeZoneInfo.Local);
                            remindEntity.EndTime = endTime;
                            remindRepository.Create(remindEntity);
                            await botClient.SendTextMessageAsync(message.ChatId, "Создал напоминание");
                            remindEntity.SetState(RemindEntity.State.Created);
                            _remindService.TryAddToRemindsSequence(remindEntity);
                            _remindEntities.Remove(message.FromId);
                        }

                        IsComplete = true;
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(message.ChatId, $"Вы не правильно ввели дату, напоминаю (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
