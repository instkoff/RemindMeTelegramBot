using RemindMeTelegramBotv2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemindMeTelegramBotv2.DAL
{
    public interface IDbRepository<T> where T : class, IBaseEntity
    {
        T Create(T entity);
        List<T> Get();
        T Get(string id);
        Task<T> GetByTlgId(string tlg_id);
        void Remove(string id);
        void Remove(T entityIn);
        void Update(string id, T entityIn);
    }
}