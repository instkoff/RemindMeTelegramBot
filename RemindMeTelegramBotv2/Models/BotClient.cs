using System.Collections.Generic;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    public class BotClient : IBotClient
    {
        private readonly List<string> _commandList;
        public IReadOnlyList<string> Commands => _commandList.AsReadOnly();

        public TelegramBotClient Client { get; private set; }
        
        public BotClient()
        {
            Client = GetClient().Result;
            _commandList = new List<string>
            {
                "/start",
                "/addremind",
                "/myremindslist"
            };
        }

        private async Task<TelegramBotClient> GetClient()
        {
            var botclient = new TelegramBotClient(BotSettings.Key,new HttpToSocks5Proxy());
            await botclient.DeleteWebhookAsync();
            await botclient.SetWebhookAsync(BotSettings.Url);
            return botclient;
        }
    }
}
