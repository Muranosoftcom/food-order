using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contexts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Repositories
{
    public class FoodOrderRepository : IRepository
    {
        private FoodOrderContext _context;

        public FoodOrderRepository(FoodOrderContext context)
        {
            _context = context;
        }

        public IQueryable<T> All<T>() where T : Entity
        {
            return _context.Set<T>().AsQueryable();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }                

        public Task InsertAsync<T>(T entity) where T : Entity
        {
            return _context.Set<T>().AddAsync(entity);
        }
        
        public Task InsertAsync<T>(IEnumerable<T> entities) where T : Entity
        {
            return _context.Set<T>().AddRangeAsync(entities);
        }

        public void Delete<T>(T entity) where T : Entity
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Update(entity);
        }
        
        public void Update<T>(IEnumerable<T> entities) where T : Entity
        {
            _context.UpdateRange(entities);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Save() {
            _context.SaveChanges();
        }
    }
}