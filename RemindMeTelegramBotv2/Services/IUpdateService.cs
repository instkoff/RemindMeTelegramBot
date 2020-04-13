using System.Threading.Tasks;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.Services
{
    /// <summary>
    /// Интерфейс для работы с апдейтами полученными от бота
    /// </summary>
    public interface IUpdateService
    {
        //Task AnswerOnMessageAsync(Message update);
        //Task AnswerOnCallbackQueryAsync(CallbackQuery update);
        Task AnswerOn(MessageDetails messageDetails);
    }
}
