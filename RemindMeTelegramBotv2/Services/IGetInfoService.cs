using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Services
{
    /// <summary>
    /// Интерфейс для работы с сервисом
    /// </summary>
    public interface IGetInfoService
    {
        Task<WebhookInfo> GetWebHookInfo();
        Task<User> GetUserInfo();
    }
}
