using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemindMeTelegramBotv2.Services;
using Telegram.Bot.Types;

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
            await _updateService.AnswerAsync(update);
            return Ok();
        }
    }
}
