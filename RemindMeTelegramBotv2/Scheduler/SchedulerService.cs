using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using RemindMeTelegramBotv2.Scheduler.Jobs;

namespace RemindMeTelegramBotv2.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IServiceProvider _serviceProvider;
        private QuartzScheduler _quartzScheduler;
        private readonly ILogger _logger;


        public SchedulerService(IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger(nameof(SchedulerService));
        }

        public async Task Init()
        {
            _quartzScheduler = new QuartzScheduler();

            var factory = new StdSchedulerFactory();
            _quartzScheduler.Scheduler = await factory.GetScheduler();
            _quartzScheduler.Scheduler.JobFactory = new JobFactory(_serviceProvider);

            _quartzScheduler.Job = JobBuilder.Create<FillRemindsList>()
                .WithIdentity("FillRemindList", "grp")
                .Build();

            _quartzScheduler.Trigger = TriggerBuilder.Create()
                .WithIdentity("updateTrigger", "crwl")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(8)
                    .RepeatForever())
                .Build();
            _logger.LogInformation("Scheduler initialized");
        }

        public async Task Start()
        {
            await Init();

            if (_quartzScheduler.Scheduler.IsStarted)
            {
                _logger.LogError("Scheduler is already running");
                return;
            }

            await _quartzScheduler.Scheduler.Start();
            await _quartzScheduler.Scheduler.ScheduleJob(_quartzScheduler.Job, _quartzScheduler.Trigger);

            _logger.LogInformation("Scheduler was started");
        }

        public async Task Stop()
        {
            if (_quartzScheduler.Scheduler == null)
            {
                _logger.LogError("Scheduler was null");
                return;
            }

            if (_quartzScheduler.Scheduler.IsShutdown)
            {
                _logger.LogError("Scheduler is already stopped");
                return;
            }

            await _quartzScheduler.Scheduler.Shutdown();
            _logger.LogInformation("Scheduler was stopped");
        }

        public async Task Restart()
        {
            if (_quartzScheduler.Scheduler.IsStarted)
            {
                await _quartzScheduler.Scheduler.Shutdown();
                _logger.LogInformation("Scheduler was stopped");
            }

            // Если мы полностью выключаем планировщик то необходимо переинитить шедуллер и тригер
            await Init();

            await _quartzScheduler.Scheduler.Start();
            await _quartzScheduler.Scheduler.ScheduleJob(_quartzScheduler.Job, _quartzScheduler.Trigger);

            _logger.LogInformation("Scheduler was restarted");
        }
    }
}
