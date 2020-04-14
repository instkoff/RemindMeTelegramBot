namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Различные состояния команд
    /// </summary>
    public enum CommandState
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4
    }
    public enum RemindState
    {
        Creation,
        Created,
        InQueue,
        Completed
    }
}
