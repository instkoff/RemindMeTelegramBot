namespace RemindMeTelegramBotv2.Models
{
    interface IRemindBuilder
    {
        void StartStage(MessageDetails message);
        bool EnterTimeZone(MessageDetails message);
        void EnterTextStage(MessageDetails message);
        bool EnterDateStage(MessageDetails message);
    }
}
