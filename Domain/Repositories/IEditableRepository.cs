using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IEditableRepository<T> : IRepository<T>
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}