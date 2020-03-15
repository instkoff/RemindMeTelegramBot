namespace RemindMeTelegramBotv2.Services
{
    public interface IRemindService
    {
        void InitializeTimers();
        void FillRemindsList(object obj);
    }
}