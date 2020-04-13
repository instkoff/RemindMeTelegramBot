using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2.Controllers
{
    /// <summary>
    /// Контроллер проверки работоспособности бота
    /// </summary>
    [Route("api/getinfo")]
    public class GetInfoController :Controller
    {
        private readonly IGetInfoService _infoService;

        public GetInfoController(IGetInfoService infoService)
        {
            _infoService = infoService;
        }
        [HttpGet]
        public async Task<IActionResult> GetHookInfo()
        {
            var result = await _infoService.GetWebHookInfo();
            return Ok(result);
        }
        [HttpGet("get_user")]
        public async Task<IActionResult> GetUserInfo()
        {
            var result = await _infoService.GetUserInfo();
            return Ok(result);
        }
    }
}
