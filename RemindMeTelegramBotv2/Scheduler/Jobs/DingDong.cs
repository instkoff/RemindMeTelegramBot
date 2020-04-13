using System;
using System.Threading.Tasks;
using Quartz;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using RemindMeTelegramBotv2.Models.Commands;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2.Scheduler.Jobs
{
    /// <summary>
    /// Джоба срабатывания напоминания
    /// </summary>
    public class DingDong : IJob
    {
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;
        private readonly IRemindService _remindService;
        public DingDong(IBotClient botClient, IDbRepository<RemindEntity> dbRepository, IRemindService remindService)
        {
            _botClient = botClient;
            _dbRepository = dbRepository;
            _remindService = remindService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now.ToUniversalTime();
            if (_remindService.CurrentReminds.Count > 0)
            {
                foreach (var remind in _remindService.CurrentReminds)
                {
                    if (remind.EndTime <= now)
                    {
                        await _botClient.Client.SendTextMessageAsync(remind.TelegramChatId, remind.RemindText);
                        remind.State = RemindState.Completed;
                        _dbRepository.Update(remind.Id, remind);
                    }
                }
                _remindService.CurrentReminds.RemoveAll(r => r.State == RemindState.Completed);
            }
        }
    }
}
