﻿using MongoDB.Driver;

namespace RemindMeTelegramBotv2.DAL
{
    /// <summary>
    /// Класс контекста ДБ
    /// </summary>
    public class DbContext : IDbContext
    {
        private readonly IMongoDatabase _db;

        public DbContext(IDatabaseSettings dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.ConnectionString);
            _db = mongoClient.GetDatabase(dbSettings.DatabaseName);
        }
        //Получаем коллекцию
        public IMongoCollection<T> GetCollection<T>(string name) where T :class
        {
            return _db.GetCollection<T>(name);
        }

    }
}
