using System.Threading.Tasks;

namespace RemindMeTelegramBotv2.Scheduler
{
    /// <summary>
    /// Интерфейс для работы с шедулером
    /// </summary>
    public interface ISchedulerService
    {
        Task Init();
        Task Start();
        Task Stop();
        Task Restart();
    }
}