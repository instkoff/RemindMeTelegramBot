using MongoDB.Driver;

namespace RemindMeTelegramBotv2.DAL
{
    public interface IDbContext
    {
        IMongoCollection<T> GetCollection<T>(string name) where T : class;
    }
}