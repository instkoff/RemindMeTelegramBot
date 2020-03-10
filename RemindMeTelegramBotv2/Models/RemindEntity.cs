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

        public RemindEntity(string tlgId) : base(tlgId)
        {
            CurrentTime = DateTime.Now;
            tlgId = "";
        }

        public RemindEntity(DateTime alarmTime, DateTime currentTime, string remindText, string tlgId) : base(tlgId)
        {
            AlarmTime = alarmTime;
            CurrentTime = currentTime;
            RemindText = remindText;
        }
    }
}