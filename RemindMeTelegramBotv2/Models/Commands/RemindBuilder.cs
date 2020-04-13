using System;
using System.Globalization;

namespace RemindMeTelegramBotv2.Models.Commands
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
            _remind.State = RemindState.EnterText;
        }

        public void EnterTextStage(MessageDetails message)
        {
            _remind.RemindText = message.MessageText;
            _remind.State = RemindState.EnterDate;
        }

        public bool EnterDateStage(MessageDetails message)
        {
            string[] formats =
            {
                "dd/MM/yyyy HH:mm", "dd-MM-yyyy HH:mm", "dd:MM:yyyy HH:mm", "dd.MM.yyyy HH:mm",
                "dd/MM/yy HH:mm"
            };

            if (DateTime.TryParseExact(message.MessageText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var outDate))
            {
                _remind.EndTime = outDate.ToUniversalTime();
                _remind.State = RemindState.Created;
                return true;
            }
            else
            {
                return false;
            }
        }

        public RemindEntity GetRemindEntity()
        {
            RemindEntity remind = _remind;
            Reset();
            return remind;
        }
    }
}
