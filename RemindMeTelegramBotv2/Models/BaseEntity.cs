using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RemindMeTelegramBotv2.Models
{
    /// <summary>
    /// Базовые свойства для всех сущностей.
    /// </summary>
    public class BaseEntity : IBaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
