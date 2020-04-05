using System.Threading.Tasks;

namespace RemindMeTelegramBotv2.Scheduler
{
    public interface ISchedulerService
    {
        Task Init();
        Task Start();
        Task Stop();
        Task Restart();
    }
}