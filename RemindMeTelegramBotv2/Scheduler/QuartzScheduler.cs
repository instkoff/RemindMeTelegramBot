using Quartz;

namespace RemindMeTelegramBotv2.Scheduler
{
    public class QuartzScheduler
    {
        /// <summary>
        ///     Wrapper для работы с планировщиком(Quartz)
        /// </summary>
        public IScheduler Scheduler { get; set; }

        public ITrigger Trigger { get; set; }

        public IJobDetail Job { get; set; }
    }
}
