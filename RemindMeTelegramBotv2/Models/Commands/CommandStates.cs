namespace RemindMeTelegramBotv2.Models.Commands
{
    /// <summary>
    /// Различные состояния команд
    /// </summary>
    public enum States
    {
        Stage1,
        Stage2
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
