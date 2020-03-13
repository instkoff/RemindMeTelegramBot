﻿using MongoDB.Bson;
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
        public int TelegramUsernameId { get; set; }

        public State state;

        public enum State
        {
            Start,
            EnterText,
            EnterDate
        }

        public RemindEntity(int usernameId, string username)
        {
            state = State.Start;
            TelegramUsernameId = usernameId;
            TelegramUsername = username;
        }

        public State GetState()
        {
            return state;
        }
        public void SetState(State currentState)
        {
            state = currentState;
        }

    }
}