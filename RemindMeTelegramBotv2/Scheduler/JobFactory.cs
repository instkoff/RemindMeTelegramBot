using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

namespace RemindMeTelegramBotv2.Scheduler
{
    /// <summary>
    /// Создатель джоб
    /// </summary>
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///     Создание новой джобы
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="scheduler"></param>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var scope = _serviceProvider.CreateScope();
            return (IJob)scope.ServiceProvider.GetService(bundle.JobDetail.JobType);
        }


        public void ReturnJob(IJob job)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
