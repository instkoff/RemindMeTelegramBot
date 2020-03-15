using MongoDB.Driver;
using RemindMeTelegramBotv2.DAL;
using RemindMeTelegramBotv2.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RemindMeTelegramBotv2.Services
{
    public class RemindService : IRemindService
    {
        private readonly IDbRepository<RemindEntity> _dbRepository;
        private readonly IBotClient _botClient;
        //List<RemindEntity> _currentReminds = new List<RemindEntity>();
        static Queue<RemindEntity> _queueOfReminds = new Queue<RemindEntity>();

        public RemindService(IDbRepository<RemindEntity> dbRepository, IBotClient botClient)
        {
            _dbRepository = dbRepository;
            _botClient = botClient;
        }
        public RemindService(IDbRepository<RemindEntity> dbRepository)
        {
            _dbRepository = dbRepository;
        }

        private void StartReminding(object obj)
        {
            DingDong();
        }

        public async void FillRemindsList(object obj)
        {
            _queueOfReminds.Clear();
            var remindsList = await _dbRepository.GetFiltered(r => r.EndTime <= DateTime.Now.AddDays(1) && r.EndTime >= DateTime.Now).ToListAsync();
            remindsList.Sort((a, b) => a.EndTime.CompareTo(b.EndTime));
            foreach (var remind in remindsList)
            {
                _queueOfReminds.Enqueue(remind);
            }

        }


        private void DingDong()
        {
            if (_queueOfReminds.Count > 0)
            {
                while (_queueOfReminds.Peek().EndTime <= DateTime.Now.ToUniversalTime())
                {
                    var remind = _queueOfReminds.Dequeue();
                    _botClient.Client.SendTextMessageAsync(remind.TelegramChatId, remind.RemindText);
                    _queueOfReminds.TrimExcess();
                }
            }
        }

        public void InitializeTimers()
        {
            TimerCallback tm2 = new TimerCallback(FillRemindsList);
            Timer timer2 = new Timer(tm2, null, 0, 60 * 60 * 1000);
            TimerCallback tm = new TimerCallback(StartReminding);
            Timer timer = new Timer(tm, null, 0, 30000);

        }


    }
}
