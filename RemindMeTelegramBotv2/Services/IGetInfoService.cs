using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Services
{
    public interface IGetInfoService
    {
        Task<WebhookInfo> GetWebHookInfo();
        Task<User> GetUserInfo();
    }
}
