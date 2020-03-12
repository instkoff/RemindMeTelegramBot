using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemindMeTelegramBotv2.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RemindMeTelegramBotv2.Controllers
{
    [Route("api/listener")]
    public class ListenerController : Controller
    {
        private readonly IUpdateService _updateService;

        public ListenerController(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        [HttpPost]
        public async Task<IActionResult> PostUpdate([FromBody]Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    await _updateService.AnswerOnMessageAsync(update);
                    break;
                case UpdateType.CallbackQuery:
                    await _updateService.AnswerOnCallbackQueryAsync(update);
                    break;
            }
            return Ok();
        }
    }
}
