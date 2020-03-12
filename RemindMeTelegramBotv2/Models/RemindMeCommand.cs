using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindMeCommand : Command
    {
        public override string Name => "/addremind";

        public override async Task ExecuteAsync(TelegramBotClient botClient, Message message, IDbRepository<RemindEntity> remindRepository)
        {
            base.isComplete = false;
            RemindEntity.State state = RemindEntity.State.Start;
            var remindEntity = remindRepository.Get(r => r.TelegramUsernameId == message.From.Id.ToString());
            if(remindEntity != null)
                state = remindEntity.GetState();
            if (message.Text == "/reset")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Отмена...");
                remindRepository.Remove(remindEntity.Id);
                base.isComplete = true;
                return;
            }
            switch (state)
            {
                case RemindEntity.State.Start:
                    var newRemind = new RemindEntity(message.From.Id.ToString(), message.From.Username);
                    newRemind.CurrentTime = DateTime.Now;
                    newRemind.RemindText = "";
                    await botClient.SendTextMessageAsync(message.Chat.Id, "О чём вам напомнить? (Сброс диалога, команда /reset)");
                    newRemind.SetState(RemindEntity.State.EnterText);
                    remindRepository.Create(newRemind);
                    break;
                case RemindEntity.State.EnterText:
                    remindEntity.RemindText = message.Text;
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Когда напомнить? (Введите в формате дд.мм.гггг чч:мм)\n Например: {DateTime.Now:dd.MM.yyyy HH:mm})");
                    remindEntity.SetState(RemindEntity.State.EnterDate);
                    remindRepository.Update(remindEntity.Id,remindEntity);
                    break;
                case RemindEntity.State.EnterDate:
                    DateTime outDate;
                    string[] formats = { "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd:MM:yyyy HH:mm", "dd.MM.yyyy HH:mm",
                        "dd/MM/yy HH:mm" };
                    if (DateTime.TryParseExact(message.Text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out outDate))
                    {
                        remindEntity.AlarmTime = outDate;
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Создал напоминание");
                        remindEntity.SetState(RemindEntity.State.Start);
                        remindRepository.Update(remindEntity.Id, remindEntity);
                        base.isComplete = true;
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Что то вы ввели не так... Попробуете ещё?");
                        return;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
