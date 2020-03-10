﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RemindMeTelegramBotv2.Models
{
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}