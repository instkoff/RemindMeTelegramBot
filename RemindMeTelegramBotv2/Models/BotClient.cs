using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    public class BotClient : IBotClient
    {
        private TelegramBotClient _client;
        public TelegramBotClient Client { get; }

        public BotClient()
        {
            _client = GetClient();
            Client = _client;
        }

        private TelegramBotClient GetClient()
        {
            var botclient = new TelegramBotClient(BotSettings.Key, new HttpToSocks5Proxy("110.49.101.58", 1080));
            botclient.DeleteWebhookAsync();
            botclient.SetWebhookAsync(BotSettings.Url);
            return botclient;
        }
    }
}
