using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> All();
        T GetById(int id);
        
    }
}