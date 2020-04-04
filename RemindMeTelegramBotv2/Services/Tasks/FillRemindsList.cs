using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.Services.Tasks
{
    public class FillRemindsList : IJob
    {
        private readonly IDbRepository<RemindEntity> _dbRepository;
        private readonly IBotClient _botClient;
        static List<RemindEntity> _currentReminds;

        public FillRemindsList(IDbRepository<RemindEntity> dbRepository, IBotClient botClient)
        {
            _dbRepository = dbRepository;
            _botClient = botClient;
            _currentReminds = new List<RemindEntity>();
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now.ToUniversalTime();
            var nowPlusDay = DateTime.Now.ToUniversalTime().AddDays(1);
            var remindsList = await _dbRepository.GetListAsync(r => r.EndTime <= nowPlusDay && r.EndTime >= now);
            foreach (var remind in remindsList)
            {
                if (!(_currentReminds.Contains(remind)))
                {
                    _currentReminds.Add(remind);
                }
            }
            _currentReminds.Sort((a, b) => a.EndTime.CompareTo(b.EndTime));
            _dbRepository.RemoveMany(r => r.state == RemindEntity.State.Completed);
        }
    }
}
