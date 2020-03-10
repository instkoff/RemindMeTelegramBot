using System;
using RemindMeTelegramBotv2.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RemindMeTelegramBotv2.DAL
{
    public interface IDbRepository<T> where T : class, IBaseEntity
    {
        T Create(T entity);
        T Get(Expression<Func<T, bool>> predicate);
        T Get(string id);
        //Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        void Remove(string id);
        void Remove(T entityIn);
        void Update(string id, T entityIn);
    }
}