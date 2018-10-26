using System.Linq;
using System.Threading.Tasks;
using Domain.Contexts;

namespace Domain.Repositories
{
    public abstract class EditableRepositoryBase<T> : IEditableRepository<T>
    {
        protected IFoodOrderContext _context;
        
        public EditableRepositoryBase(IFoodOrderContext context)
        {
            _context = _context;
        }

        public abstract IQueryable<T> All();

        public abstract T GetById(int id);

        public abstract Task Insert(T entity);

        public abstract Task Update(T entity);

        public abstract Task Delete(T entity);
    }
}