using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class CommandsCreator : ICommandsCreator
    {
        private readonly IRemindService _remindService;
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;
        public CommandsCreator(IRemindService remindService, IBotClient botClient, IDbRepository<RemindEntity> dbRepository)
        {
            _remindService = remindService;
            _botClient = botClient;
            _dbRepository = dbRepository;
        }
        public Command CreateCommand(string commandName)
        {
            switch (commandName)
            {
                case "/start":
                    return new StartCommand(_botClient);
                case "/addremind":
                    return new RemindMeCommand(_remindService, _botClient, _dbRepository);
                case "/myremindslist":
                    return new MyRemindsListCommand(_botClient,_dbRepository);
                default:
                    return null;
            }
        }
    }
}
