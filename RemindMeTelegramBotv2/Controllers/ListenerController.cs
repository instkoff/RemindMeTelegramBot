using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemindMeTelegramBotv2.Models;
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
            MessageDetails messageDetails;
            switch (update.Type)
            {
                case UpdateType.Message:
                    var usernameM = update.Message.From.Username ?? update.Message.From.FirstName;
                    messageDetails = new MessageDetails(update.Message.From.Id, update.Message.Chat.Id, usernameM, update.Message.Text);
                    await _updateService.AnswerOn(messageDetails);
                    break;
                case UpdateType.CallbackQuery:
                    var usernameC = update.CallbackQuery.From.Username ?? update.CallbackQuery.From.FirstName;
                    messageDetails = new MessageDetails(update.CallbackQuery.From.Id, update.CallbackQuery.Message.Chat.Id, usernameC, update.CallbackQuery.Data);
                    await _updateService.AnswerOn(messageDetails);
                    break;
            }
            return Ok();
        }
    }
}
