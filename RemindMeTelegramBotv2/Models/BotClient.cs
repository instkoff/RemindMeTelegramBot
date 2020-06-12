using System.Collections.Generic;
using System.Threading.Tasks;
using MihaZupan;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    /// <summary>
    /// Класс инициализации бота
    /// </summary>
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
                //Список имеющихся у бота команд
                "/start",
                "/send_keyboard",
                "/addremind",
                "/myremindslist",
                "/delremind"
            };
        }
        //Инициализация бота
        private async Task<TelegramBotClient> GetClient()
        {
            var botclient = new TelegramBotClient(BotSettings.Key,new HttpToSocks5Proxy("87.247.143.226", 54840, "NUTvEKUJUi", "5TM7frYuzI"));
            await botclient.DeleteWebhookAsync();
            await botclient.SetWebhookAsync(BotSettings.Url);
            return botclient;
        }
    }
}
