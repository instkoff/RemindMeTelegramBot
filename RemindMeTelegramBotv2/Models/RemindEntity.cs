using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Globalization;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindEntity : BaseEntity
    {
        public DateTime AlarmTime { get; set; }
        public DateTime CurrentTime { get; set; }
        public string RemindText { get; set; }
        public string TelegramChatId { get; set; }
        public string TelegramUsername { get; set; }
        public string TelegramId { get; set; }

        public int stage;


        public RemindEntity(string id, string chatId, string username)
        {
            stage = 1;
            TelegramId = id;
            TelegramChatId = chatId;
            TelegramUsername = username;
        }

        public (string, int) StageText(string id)
        {
            if (stage == 1)
                return ("Введите текст напомнинания", stage);
            else
                return ("Введите время когда вам напомнить", stage);
        }
        public bool SetParam(string param)
        {
            if (stage == 1)
                RemindText = param;
            if (stage == 2)
                AlarmTime = DateTime.ParseExact(param, "dd:MM:yyyy HH:mm", CultureInfo.InvariantCulture);
            stage++;
            return true;
        }

    }
}