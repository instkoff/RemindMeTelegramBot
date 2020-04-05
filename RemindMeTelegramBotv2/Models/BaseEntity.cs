using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RemindMeTelegramBotv2.Scheduler;

namespace RemindMeTelegramBotv2.Models
{
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
