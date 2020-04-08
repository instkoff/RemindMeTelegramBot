using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2.Models.Commands
{
    public class CommandsCreator : ICommandsCreator
    {
        private readonly IRemindService _remindService;
        public CommandsCreator(IRemindService remindService)
        {
            _remindService = remindService;
        }
        public Command CreateCommand(string commandName)
        {
            switch (commandName)
            {
                case "/start":
                    return new StartCommand();
                case "/addremind":
                    return new RemindMeCommand(_remindService);
                case "/myremindslist":
                    return new MyRemindsListCommand();
                default:
                    return null;
            }
        }
    }
}
