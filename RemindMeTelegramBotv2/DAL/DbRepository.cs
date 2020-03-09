using MongoDB.Driver;
using RemindMeTelegramBotv2.Models;
using System.Collections.Generic;

namespace RemindMeTelegramBotv2.DAL
{
    public class DbRepository<T> : IDbRepository<T> where T : class, IBaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public DbRepository(IDatabaseSettings dbsettings)
        {
            var client = new MongoClient(dbsettings.ConnectionString);
            var database = client.GetDatabase(dbsettings.DatabaseName);
            var table = typeof(T).Name;
            _collection = database.GetCollection<T>(table);
        }

        public List<T> Get() =>
            _collection.Find(entity => true).ToList();

        public T Get(string id) =>
            _collection.Find<T>(entity => entity.Id == id).FirstOrDefault();

        public T Create(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }

        public void Update(string id, T entityIn) =>
            _collection.ReplaceOne(entity => entity.Id == id, entityIn);

        public void Remove(T entityIn) =>
            _collection.DeleteOne(entity => entity.Id == entityIn.Id);

        public void Remove(string id) =>
            _collection.DeleteOne(entity => entity.Id == id);
    }
}
