using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Globalization;

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

        public event Action<object> onCreated;

        public State state;

        public enum State
        {
            Start,
            EnterText,
            EnterDate,
            Created,
            Completed
        }

        public RemindEntity(int usernameId, string username, long chatId)
        {
            state = State.Start;
            TelegramUsernameId = usernameId;
            TelegramUsername = username;
            TelegramChatId = chatId;
        }

        public State GetState()
        {
            return state;
        }
        public void SetState(State currentState)
        {
            state = currentState;
            if (state == State.Created)
            {

                onCreated?.Invoke(null);
            }
        }

        public override string ToString()
        {
            return $"{EndTime} напомнить о: {RemindText} \n";
        }
    }
}