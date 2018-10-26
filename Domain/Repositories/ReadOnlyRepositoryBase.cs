using System.Linq;
using Domain.Contexts;

namespace Domain.Repositories
{
    public class ReadOnlyRepositoryBase<T> : IRepository<T>
    {
        protected IFoodOrderContext _context;
        
        public ReadOnlyRepositoryBase(IFoodOrderContext context)
        {
            _context = _context;
        }

        public IQueryable<T> All()
        {
            throw new System.NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}