using System.Collections.Generic;
using System.Threading.Tasks;
using MihaZupan;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    public class BotClient : IBotClient
    {

        public TelegramBotClient Client { get; private set; }

        private List<Command> commandsList;
        public  IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public BotClient()
        {
            Client = GetClient().Result;
            commandsList = new List<Command>
            {
                new RemindMeCommand(),
                new SendKeyboardCommand()
            };
        }

        private async Task<TelegramBotClient> GetClient()
        {
            var botclient = new TelegramBotClient(BotSettings.Key,new HttpToSocks5Proxy("110.49.101.58", 1080));
            await botclient.DeleteWebhookAsync();
            await botclient.SetWebhookAsync(BotSettings.Url);
            return botclient;
        }
    }
}
