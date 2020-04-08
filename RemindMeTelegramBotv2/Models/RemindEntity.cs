using System;

namespace RemindMeTelegramBotv2.Models
{
    public class RemindEntity : BaseEntity
    {
        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public string RemindText { get; set; }
        public string TelegramUsername { get; set; }
        public long TelegramChatId { get; set; }
        public int TelegramUsernameId { get; set; }

        public States State { get; set; }

        public enum States
        {
            Start,
            EnterText,
            EnterDate,
            Created,
            InQueue,
            Completed
        }

        public RemindEntity(int usernameId, string username, long chatId)
        {
            State = States.Start;
            TelegramUsernameId = usernameId;
            TelegramUsername = username;
            TelegramChatId = chatId;
        }


        public override string ToString()
        {
            return $"{EndTime} напомнить о: {RemindText} \n";
        }
    }
}