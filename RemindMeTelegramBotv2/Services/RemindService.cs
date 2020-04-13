using System;
using System.Collections.Generic;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.Services
{
    /// <summary>
    /// Сервис для работы с напоминаниями
    /// </summary>
    public class RemindService : IRemindService
    {
        public List<RemindEntity> CurrentReminds { get; }

        public RemindService()
        {
            CurrentReminds = new List<RemindEntity>();
        }

        /// <summary>
        /// Попытка добавить вновь созданное напоминание с текущий список
        /// </summary>
        /// <param name="newRemindEntity"></param>

        public void TryAddToRemindsSequence(RemindEntity newRemindEntity)
        {
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Utc);
            var nowPlusDay = TimeZoneInfo.ConvertTime(DateTime.Now.AddDays(1), TimeZoneInfo.Utc);
            if (newRemindEntity != null)
            {
                if(newRemindEntity.EndTime <= nowPlusDay && newRemindEntity.EndTime >= now)
                {
                    CurrentReminds.Add(newRemindEntity);
                }
            }
        }
    }
}
