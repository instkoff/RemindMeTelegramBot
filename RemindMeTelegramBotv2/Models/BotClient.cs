using System.Collections.Generic;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    public class BotClient : IBotClient
    {
        private readonly TelegramBotClient _client;
        public TelegramBotClient Client { get; }
        private List<ICommand> commandsList;
        public  IReadOnlyList<ICommand> Commands
        {
            get => commandsList.AsReadOnly();
        }

        public BotClient()
        {
            _client = GetClient();
            Client = _client;
            commandsList = new List<ICommand>();
            commandsList.Add(new RemindMeCommand());
        }

        private TelegramBotClient GetClient()
        {
            var botclient = new TelegramBotClient(BotSettings.Key,new HttpToSocks5Proxy("110.49.101.58", 1080));
            botclient.DeleteWebhookAsync();
            botclient.SetWebhookAsync(BotSettings.Url);
            return botclient;
        }
    }
}
