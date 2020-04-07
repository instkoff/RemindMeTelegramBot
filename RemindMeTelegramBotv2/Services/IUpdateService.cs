using System.Threading.Tasks;
using RemindMeTelegramBotv2.Models;
using Telegram.Bot.Types;

namespace RemindMeTelegramBotv2.Services
{
    public interface IUpdateService
    {
        //Task AnswerOnMessageAsync(Message update);
        //Task AnswerOnCallbackQueryAsync(CallbackQuery update);
        Task AnswerOn(MessageDetails messageDetails);
    }
}
