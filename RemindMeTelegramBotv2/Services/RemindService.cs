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
        static List<RemindEntity> _currentReminds;
        private Timer _fillTimer;
        private Timer _remindTimer;
        //static Queue<RemindEntity> _queueOfReminds = new Queue<RemindEntity>();

        public RemindService(IDbRepository<RemindEntity> dbRepository, IBotClient botClient)
        {
            _dbRepository = dbRepository;
            _botClient = botClient;
            _currentReminds = new List<RemindEntity>();
        }

        private void StartReminding(object obj)
        {
            DingDong();
        }

        private async void FillRemindsList(object obj)
        {
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
            var nowPlusDay = TimeZoneInfo.ConvertTime(DateTime.Now.AddDays(1), TimeZoneInfo.Utc);
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

        public void TryAddToRemindsSequence(object obj)
        {
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
            var nowPlusDay = TimeZoneInfo.ConvertTime(DateTime.Now.AddDays(1), TimeZoneInfo.Utc);
            if (obj != null)
            {
                var remind = (RemindEntity)obj;
                if(remind.EndTime <= nowPlusDay && remind.EndTime >= now)
                {
                    _currentReminds.Add(remind);
                }
            }
        }

        private async void DingDong()
        {
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
            if (_currentReminds.Count > 0)
            {
                foreach (var remind in _currentReminds)
                {
                    if (remind.EndTime <= now)
                    {
                        await _botClient.Client.SendTextMessageAsync(remind.TelegramChatId, remind.RemindText);
                        remind.SetState(RemindEntity.State.Completed);
                        _dbRepository.Update(remind.Id, remind);
                    }
                }
                _currentReminds.RemoveAll(r => r.state == RemindEntity.State.Completed);
            }

        }

        public void InitializeTimers()
        {
            TimerCallback tm2 = new TimerCallback(FillRemindsList);
            _fillTimer = new Timer(tm2, null, 0, 60 * 60 * 1000);
            TimerCallback tm = new TimerCallback(StartReminding);
            _remindTimer = new Timer(tm, null, 0, 30000);

        }


    }
}
