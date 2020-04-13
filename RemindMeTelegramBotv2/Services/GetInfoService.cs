using System.Threading.Tasks;
using RemindMeTelegramBotv2.Models;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Services
{
    /// <summary>
    /// Сервис получения информации от бота
    /// </summary>
    public class GetInfoService : IGetInfoService
    {
        private readonly IBotClient _botClient;
        public GetInfoService(IBotClient client)
        {
            _botClient = client;
        }
        public async Task<WebhookInfo> GetWebHookInfo()
        {
            return await _botClient.Client.GetWebhookInfoAsync();
        }
        public async Task<User> GetUserInfo()
        {
            return await _botClient.Client.GetMeAsync();
        }
    }
}
