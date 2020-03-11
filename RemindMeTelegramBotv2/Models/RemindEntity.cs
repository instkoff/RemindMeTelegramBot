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
        public string TelegramUsername { get; set; }
        public string TelegramUsernameId { get; set; }

        public int stage;


        public RemindEntity(string usernameId, string username)
        {
            stage = 1;
            TelegramUsernameId = usernameId;
            TelegramUsername = username;
        }

        public (string, int) StageText(string id)
        {
            if (stage == 1)
                return ("Введите текст напомнинания", stage);
            if (stage == 2)
                return ("Введите время когда вам напомнить", stage);
            else
                return("Создал напоминание", stage);
        }
        public bool SetParam(string param)
        {
            if (stage == 1)
                RemindText = param;
            if (stage == 2)
            {
                DateTime outDate;
                string[] formats = { "dd/MM/yyyy hh:mm", "dd-MM-yyyy hh:mm", "dd:MM:yyyy hh:mm", "dd.MM.yyyy hh:mm",
                    "dd/MM/yy hh:mm"};
                if (DateTime.TryParseExact(param, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out outDate))
                {
                    AlarmTime = outDate;
                }
                else
                {
                    return false;
                }
            }
            stage++;
            return true;
        }

    }
}