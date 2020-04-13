namespace RemindMeTelegramBotv2.Models.Commands
{
    interface IRemindBuilder
    {
        void StartStage(MessageDetails message);
        void EnterTextStage(MessageDetails message);
        bool EnterDateStage(MessageDetails message);
    }
}
