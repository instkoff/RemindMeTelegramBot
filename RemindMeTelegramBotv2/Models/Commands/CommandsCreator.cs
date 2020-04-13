using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Класс-создатель команд, создаёт конкретную команду бота и прокидывает в неё необходимые зависимости
    /// </summary>
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
                    return new StartCommand(_botClient.Client);
                case "/addremind":
                    return new RemindMeCommand(_remindService, _botClient.Client, _dbRepository);
                case "/myremindslist":
                    return new MyRemindsListCommand(_botClient.Client,_dbRepository);
                case "/delremind":
                    return new DeleteRemindCommand(_botClient.Client, _dbRepository, this);
                default:
                    return null;
            }
        }
    }
}
