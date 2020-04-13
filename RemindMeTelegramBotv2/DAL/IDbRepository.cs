using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RemindMeTelegramBotv2.Models;

namespace RemindMeTelegramBotv2.DAL
{
    /// <summary>
    ///         Интерфейс для работы с репозиторием
    /// </summary>
    /// <typeparam name="T">RemindEntity</typeparam>
    public interface IDbRepository<T> where T : class, IBaseEntity
    {
        T Create(T entity);
        T Get(Expression<Func<T, bool>> predicate);
        T Get(string id);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        void RemoveMany(Expression<Func<T, bool>> predicate);
        void Remove(string id);
        void Remove(T entityIn);
        void Update(string id, T entityIn);
    }
}