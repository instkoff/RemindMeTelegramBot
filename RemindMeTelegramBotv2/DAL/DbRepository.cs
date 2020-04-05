using System;
using MongoDB.Driver;
using RemindMeTelegramBotv2.Scheduler;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.DAL
{
    public class DbRepository<T> : IDbRepository<T> where T: class, IBaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public DbRepository(IDbContext dbContext)
        {
            _collection = dbContext.GetCollection<T>(typeof(T).Name);
        }

        public T Get(Expression<Func<T, bool>> predicate) =>
            _collection.Find(predicate).SingleOrDefault();

        public T Get(string id) =>
            _collection.Find(entity => entity.Id == id).FirstOrDefault();

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate) =>
    await _collection.Find(predicate).ToListAsync();

        public IMongoQueryable<T> GetFiltered(Expression<Func<T, bool>> predicate)
        {
            var entity = _collection.AsQueryable().Where(predicate);
            return entity;
        }

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

        public void RemoveMany(Expression<Func<T, bool>> predicate) =>
            _collection.DeleteMany(predicate);
    }
}
