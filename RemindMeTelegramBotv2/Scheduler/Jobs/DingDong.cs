using System;
using System.Threading.Tasks;
using Quartz;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.Scheduler.Jobs
{
    public class DingDong : IJob
    {
        private readonly IBotClient _botClient;
        private readonly IDbRepository<RemindEntity> _dbRepository;
        public DingDong(IBotClient botClient, IDbRepository<RemindEntity> dbRepository)
        {
            _botClient = botClient;
            _dbRepository = dbRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now.ToUniversalTime();
            if (FillRemindsList._currentReminds.Count > 0)
            {
                foreach (var remind in FillRemindsList._currentReminds)
                {
                    if (remind.EndTime <= now)
                    {
                        await _botClient.Client.SendTextMessageAsync(remind.TelegramChatId, remind.RemindText);
                        remind.SetState(RemindEntity.State.Completed);
                        _dbRepository.Update(remind.Id, remind);
                    }
                }
                FillRemindsList._currentReminds.RemoveAll(r => r.state == RemindEntity.State.Completed);
            }
        }
    }
}
