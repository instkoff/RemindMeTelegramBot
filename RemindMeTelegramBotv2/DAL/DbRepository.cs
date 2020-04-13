using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.DAL
{
    //Репозиторий
    public class DbRepository<T> : IDbRepository<T> where T: class, IBaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public DbRepository(IDbContext dbContext)
        {
            //Получаем коллекцию
            _collection = dbContext.GetCollection<T>(typeof(T).Name);
        }
        //Получаем один элемент по фильтру
        public T Get(Expression<Func<T, bool>> predicate) =>
            _collection.Find(predicate).SingleOrDefault();
        //Получаем один элемент по Id
        public T Get(string id) =>
            _collection.Find(entity => entity.Id == id).FirstOrDefault();
        //Получаем список по фильтру
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate) =>
    await _collection.Find(predicate).ToListAsync();
        //Создать элемент
        public T Create(T entity)
        {
            _collection.InsertOne(entity);
            return entity;
        }
        //Обновить элемент
        public void Update(string id, T entityIn) =>
            _collection.ReplaceOne(entity => entity.Id == id, entityIn);
        //Удалить элемент по объекту
        public void Remove(T entityIn) =>
            _collection.DeleteOne(entity => entity.Id == entityIn.Id);
        //удалить элемент по Id
        public void Remove(string id) =>
            _collection.DeleteOne(entity => entity.Id == id);
        //Удалить много объектов по фильтру
        public void RemoveMany(Expression<Func<T, bool>> predicate) =>
            _collection.DeleteMany(predicate);
    }
}
