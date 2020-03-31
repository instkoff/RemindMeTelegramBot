using System.Collections.Generic;
using System.Threading.Tasks;
using MihaZupan;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models.Commands;
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
                new StartCommand(),
                new MyRemindsListCommand()
            };
        }

        private async Task<TelegramBotClient> GetClient()
        {
            var botclient = new TelegramBotClient(BotSettings.Key,new HttpToSocks5Proxy("92.119.161.16", 60530, "5r03ZRswaw", "9oLvv4rfMI"));
            await botclient.DeleteWebhookAsync();
            await botclient.SetWebhookAsync(BotSettings.Url);
            return botclient;
        }
    }
}
