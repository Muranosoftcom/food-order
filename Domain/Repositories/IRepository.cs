using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IRepository
    {
        IQueryable<T> All<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;
        Task InsertAsync<T>(T entity) where T : Entity;
        Task InsertAsync<T>(IEnumerable<T> entities) where T : Entity;
        void Delete<T>(T entity) where T : Entity;
        void Update<T>(T entity) where T : Entity;
        Task SaveAsync();
        void Save();
    }
}