namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Различные состояния команд
    /// </summary>
    public enum CommandState
    {
        Stage1,
        Stage2,
        Stage3
    }
    public enum RemindState
    {
        Start,
        EnterText,
        EnterDate,
        Created,
        InQueue,
        Completed
    }
}
