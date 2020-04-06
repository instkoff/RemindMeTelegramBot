using System;
using System.Text;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Scheduler.Commands
{
    public class MyRemindsListCommand : Command
    {
        public override string Name { get; } = "/myremindslist";

        public override async Task ExecuteAsync(TelegramBotClient botClient, MessageInfo message,
            IDbRepository<RemindEntity> remindRepository)
        {
            var remindsList = await remindRepository.GetListAsync(r => r.TelegramUsernameId == message.FromId && r.state != RemindEntity.State.Completed);
            if (remindsList.Count != 0)
            {
                StringBuilder remindsBuilder = new StringBuilder();
                remindsList.ForEach(r => { remindsBuilder.Append($"{r.EndTime} напомнить о: {r.RemindText} \n"); });
                await botClient.SendTextMessageAsync(message.ChatId, remindsBuilder.ToString());
                IsComplete = true;
            }
            else
            {
                await botClient.SendTextMessageAsync(message.ChatId, "Список пуст");
                IsComplete = true;
            }


        }
    }
}
