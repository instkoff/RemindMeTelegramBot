namespace RemindMeTelegramBotv2.Models.Commands
{
    public interface ICommandsCreator
    {
        Command CreateCommand(string commandName);
    }
}