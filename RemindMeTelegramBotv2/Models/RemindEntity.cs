using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindEntity : BaseEntity
    {
        public DateTime AlarmTime { get; set; }
        public DateTime CurrentTime { get; set; }
        public string RemindText { get; set; }

        public RemindEntity(DateTime alarmTime, DateTime currentTime, string remindText)
        {
            AlarmTime = alarmTime;
            CurrentTime = currentTime;
            RemindText = remindText;
        }
    }
}