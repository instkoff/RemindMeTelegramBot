using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindMeCommand : ICommand
    {
        public RemindMeCommand()
        {
            
        }
        public string Name { get; } = "Напомнить";

        public async Task ExecuteAsync(TelegramBotClient botClient, Message message)
        {
            if (message.Text.ToLower().Contains(Name.ToLower()))
            {
               var x = await botClient.SendTextMessageAsync(message.Chat.Id, "О чём вам напомнить? (Необходимо ответить на это сообщение)");
            }

            //if (botMessage != null && message.ReplyToMessage.MessageId == botMessage.MessageId)
            //{
            //    await botClient.SendTextMessageAsync(message.Chat.Id, "test");
            //}
        }
    }
}
