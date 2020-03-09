using System.Collections.Generic;
using System.Threading.Tasks;
using MihaZupan;
using RemindMeTelegramBotv2.DAL;
using Telegram.Bot;

namespace RemindMeTelegramBotv2.Models
{
    public class BotClient : IBotClient
    {
        private readonly TelegramBotClient _client;
        private readonly IDbRepository<RemindEntity> _dbRepository;
        public TelegramBotClient Client { get; }
        private List<Command> commandsList;
        public  IReadOnlyList<Command> Commands
        {
            get => commandsList.AsReadOnly();
        }

        public BotClient(IDbRepository<RemindEntity> repository)
        {
            _client = GetClient().Result;
            Client = _client;
            commandsList = new List<Command>();
            _dbRepository = repository;
            commandsList.Add(new RemindMeCommand(_dbRepository));
            commandsList.Add(new SendKeyboardCommand());
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
