using System.Text;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class MyRemindsListCommand : Command
    {
        public override string Name { get; } = "/myremindslist";

        public override async Task ExecuteAsync(TelegramBotClient botClient, MessageInfo message,
            IDbRepository<RemindEntity> remindRepository)
        {
            var remindsList = await remindRepository.GetListAsync(r => r.TelegramUsernameId == message.FromId && r.state != RemindEntity.State.Completed);
            StringBuilder remindsBuilder = new StringBuilder();
            remindsList.ForEach(r => { remindsBuilder.Append(r);});
            await botClient.SendTextMessageAsync(message.ChatId, remindsBuilder.ToString());
        }
    }
}
