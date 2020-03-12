using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RemindMeTelegramBotv2.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _remindRepository;

        private static Command _fixedCommand;

        public UpdateService(IBotClient botClient, IDbRepository<RemindEntity> remindRepository)
        {
            _remindRepository = remindRepository;
            _botClient = botClient;
        }
        public async Task AnswerOnMessageAsync(Update update)
        {
            if (update == null)
                return;


        }
        public async Task AnswerOnCallbackQueryAsync(Update update)
        {
            if (update == null)
                return;

           
        }

    }
}
