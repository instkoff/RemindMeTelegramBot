using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class RemindMeCommand : Command
    {
        public override string Name => "/addremind";

        private static Dictionary<int, RemindEntity> _remindEntities;

        public RemindMeCommand()
        {
            _remindEntities = new Dictionary<int, RemindEntity>();
        }

        public override async Task ExecuteAsync(TelegramBotClient botClient, MessageDetails message,
            IDbRepository<RemindEntity> remindRepository)
        {
            IsComplete = false;
            RemindEntity remindEntity = null;
            RemindEntity.States state = RemindEntity.States.Start;

            if (_remindEntities.ContainsKey(message.FromId))
            {
                remindEntity = _remindEntities[message.FromId];
                state = remindEntity.State;
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
                case RemindEntity.States.Start:
                    var newRemind = new RemindEntity(message.FromId, message.Username, message.ChatId)
                    {
                        StartTime = DateTime.Now.ToUniversalTime(),
                        RemindText = ""
                    };
                    await botClient.SendTextMessageAsync(message.ChatId, "О чём вам напомнить? (Сброс диалога, команда /reset)");
                    newRemind.State = RemindEntity.States.EnterText;
                    _remindEntities.Add(message.FromId,newRemind);
                    break;
                case RemindEntity.States.EnterText:
                    if (remindEntity != null)
                    {
                        remindEntity.RemindText = message.MessageText;
                        await botClient.SendTextMessageAsync(message.ChatId,
                            $"Когда напомнить? (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                        remindEntity.State = RemindEntity.States.EnterDate;
                        _remindEntities[message.FromId] = remindEntity;
                    }

                    break;
                case RemindEntity.States.EnterDate:
                    string[] formats = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd:MM:yyyy HH:mm", "dd.MM.yyyy HH:mm",
                        "dd/MM/yy HH:mm" };
                    if (DateTime.TryParseExact(message.MessageText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var outDate))
                    {
                        if (remindEntity != null)
                        {
                            remindEntity.EndTime = outDate.ToUniversalTime();
                            remindRepository.Create(remindEntity);
                            await botClient.SendTextMessageAsync(message.ChatId, "Создал напоминание");
                            remindEntity.State = RemindEntity.States.Created;
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
