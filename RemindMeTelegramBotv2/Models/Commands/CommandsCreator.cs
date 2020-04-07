namespace RemindMeTelegramBotv2.Models.Commands
{
    public class CommandsCreator : ICommandsCreator
    {
        public Command CreateCommand(string commandName)
        {
            switch (commandName)
            {
                case "/start":
                    return new StartCommand();
                case "/addremind":
                    return new RemindMeCommand();
                case "/myremindslist":
                    return new MyRemindsListCommand();
                default:
                    return null;
            }
        }
    }
}
