using System.Text;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class MyRemindsListCommand : Command
    {
        public override string Name { get; } = "/myremindslist";

        public override async Task ExecuteAsync(TelegramBotClient botClient, MessageDetails message,
            IDbRepository<RemindEntity> remindRepository)
        {
            var remindsList = await remindRepository.GetListAsync(r => r.TelegramUsernameId == message.FromId && r.State != RemindEntity.States.Completed);
            if (remindsList.Count != 0)
            {
                StringBuilder remindsBuilder = new StringBuilder();
                remindsBuilder.Append($"Ваши напоминания {message.Username}: \n");
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
