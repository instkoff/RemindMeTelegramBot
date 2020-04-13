using System;
using RemindMeTelegramBotv2.Models.Commands;

namespace RemindMeTelegramBotv2.Models
{
    /// <summary>
    /// Класс напоминания
    /// </summary>
    public class RemindEntity : BaseEntity
    {
        public DateTime EndTime { get; set; }
        public string RemindText { get; set; }
        public string TelegramUsername { get; set; }
        public long TelegramChatId { get; set; }
        public int TelegramUsernameId { get; set; }

        public RemindState State { get; set; }

        public RemindEntity()
        {
            State = RemindState.Start;
        }

        public RemindEntity(int usernameId, string username, long chatId)
        {
            State = RemindState.Start;
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