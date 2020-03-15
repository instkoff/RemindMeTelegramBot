using System;
using RemindMeTelegramBotv2.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;

namespace RemindMeTelegramBotv2.DAL
{
    public interface IDbRepository<T> where T : class, IBaseEntity
    {
        T Create(T entity);
        T Get(Expression<Func<T, bool>> predicate);
        T Get(string id);
        IMongoQueryable<T> GetFiltered(Expression<Func<T, bool>> predicate);
        void Remove(string id);
        void Remove(T entityIn);
        void Update(string id, T entityIn);
    }
}