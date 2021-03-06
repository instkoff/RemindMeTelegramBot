﻿using System;
using System.Threading.Tasks;
using Quartz;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using RemindMeTelegramBotv2.Models.Commands;
using RemindMeTelegramBotv2.Services;

namespace RemindMeTelegramBotv2.Scheduler.Jobs
{
    /// <summary>
    /// Джоба заполнения списка напоминаний
    /// </summary>
    public class FillRemindsList : IJob
    {
        private readonly IDbRepository<RemindEntity> _dbRepository;
        private readonly IRemindService _remindService;
        public FillRemindsList(IDbRepository<RemindEntity> dbRepository, IRemindService remindService)
        {
            _dbRepository = dbRepository;
            _remindService = remindService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.Now.ToUniversalTime();
            var nowPlusDay = DateTime.Now.ToUniversalTime().AddDays(1);
            var remindsList = await _dbRepository.GetListAsync(r => r.EndTime <= nowPlusDay && r.EndTime >= now);
            foreach (var remind in remindsList)
            {
                if (!(_remindService.CurrentReminds.Contains(remind)))
                {
                    _remindService.CurrentReminds.Add(remind);
                    remind.State = RemindState.InQueue;
                    _dbRepository.Update(remind.Id, remind);
                }
            }
            _remindService.CurrentReminds.Sort((a, b) => a.EndTime.CompareTo(b.EndTime));
            _dbRepository.RemoveMany(r => r.State == RemindState.Completed);
        }
    }
}
