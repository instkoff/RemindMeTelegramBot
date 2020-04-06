using Quartz;

namespace RemindMeTelegramBotv2.Scheduler
{
    public class QuartzScheduler
    {
        /// <summary>
        ///     Wrapper для работы с планировщиком(Quartz)
        /// </summary>
        public IScheduler Scheduler { get; set; }

        public ITrigger Trigger1 { get; set; }

        public IJobDetail Job1 { get; set; }

        public ITrigger Trigger2 { get; set; }

        public IJobDetail Job2 { get; set; }
    }
}
