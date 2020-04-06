using System;
using System.Collections.Generic;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.Services
{
    public class RemindService : IRemindService
    {
        public List<RemindEntity> CurrentReminds { get; }

        public RemindService()
        {
            CurrentReminds = new List<RemindEntity>();
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
                    CurrentReminds.Add(remind);
                }
            }
        }
    }
}
