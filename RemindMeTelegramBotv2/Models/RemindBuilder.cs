using System;
using System.Globalization;
using System.Text.RegularExpressions;
using RemindMeTelegramBotv2.Models.Commands;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindBuilder : IRemindBuilder
    {
        private RemindEntity _remind = new RemindEntity();

        public RemindBuilder()
        {
            Reset();
        }

        public void Reset()
        {
            _remind = new RemindEntity();
        }

        public void StartStage(MessageDetails message)
        {
            _remind.TelegramUsernameId = message.FromId;
            _remind.TelegramUsername = message.Username;
            _remind.TelegramChatId = message.ChatId;
        }

        public bool EnterTimeZone(MessageDetails message)
        {
            const string fullTimePattern = @"([+|-][0|1]\d:[0-5]\d)";
            const string shortTimePattern = @"([+|-][0|1]\d)";

            if (!Regex.IsMatch(message.MessageText, fullTimePattern) &&
                !Regex.IsMatch(message.MessageText, shortTimePattern)) 
                return false;

            var timeZonesList = TimeZoneInfo.GetSystemTimeZones();
            foreach (var timeZone in timeZonesList)
            {
                if (timeZone.ToString().Contains(message.MessageText))
                {
                    _remind.TimeZoneId = timeZone.Id;
                    return true;
                }
            }
            return false;
        }

        public void EnterTextStage(MessageDetails message)
        {
            _remind.RemindText = message.MessageText;

        }

        public bool EnterDateStage(MessageDetails message)
        {
            string[] formats =
            {
                "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd:MM:yyyy HH:mm", "dd.MM.yyyy HH:mm",
                "dd/MM/yy HH:mm"
            };

            if (!DateTime.TryParseExact(message.MessageText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var outDate)) return false;
            _remind.EndTime = outDate.ToUniversalTime();
            _remind.State = RemindState.Created;
            return true;
        }

        public RemindEntity GetRemindEntity()
        {
            RemindEntity remind = _remind;
            Reset();
            return remind;
        }

    }
}
