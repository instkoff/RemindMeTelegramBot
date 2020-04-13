namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Интерфейс создателя команд
    /// </summary>
    public interface ICommandsCreator
    {
        Command CreateCommand(string commandName);
    }
}