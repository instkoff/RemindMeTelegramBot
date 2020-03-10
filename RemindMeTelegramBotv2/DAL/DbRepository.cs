using MongoDB.Driver;
using RemindMeTelegramBotv2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemindMeTelegramBotv2.DAL
{
    public class DbRepository<T> : IDbRepository<T> where T: class, IBaseEntity
    {
        private readonly IDbContext _dbContext;
        private readonly IMongoCollection<T> _collection;

        public DbRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _collection = _dbContext.GetCollection<T>(typeof(T).Name);
        }

        public List<T> Get() =>
            _collection.Find(entity => true).ToList();

        public T Get(string id) =>
            _collection.Find<T>(entity => entity.Id == id).FirstOrDefault();

        public async Task<T> GetByTlgId(string tlg_id)
        {
           var cursor = await _collection.FindAsync<T>(entity => entity.TlgId == tlg_id);
           return cursor.Current.FirstOrDefault();
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
    }
}
