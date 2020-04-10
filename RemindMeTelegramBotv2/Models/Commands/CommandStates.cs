namespace RemindMeTelegramBotv2.Models.Commands
{
    public enum States
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
