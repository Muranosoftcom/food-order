using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;
using FoodOrder.Persistence.Contexts;

namespace FoodOrder.Persistence.Repositories {
	public class FoodOrderRepository : IFoodOrderRepository {
		private readonly FoodOrderDbContext _dbContext;

		public FoodOrderRepository(FoodOrderDbContext dbContext) {
			_dbContext = dbContext;
		}

		public IQueryable<T> All<T>() where T : Entity {
			return _dbContext.Set<T>().AsQueryable();
		}

		public T GetById<T>(Guid id) where T : Entity {
			return _dbContext.Set<T>().FirstOrDefault(x => x.Id == id);
		}

		public void Insert<T>(T entity) where T : Entity {
			_dbContext.Set<T>().Add(entity);
		}

		public Task InsertAsync<T>(T entity) where T : Entity {
			return _dbContext.Set<T>().AddAsync(entity);
		}

		public Task InsertAsync<T>(IEnumerable<T> entities) where T : Entity {
			return _dbContext.Set<T>().AddRangeAsync(entities);
		}

		public void Delete<T>(T entity) where T : Entity {
			_dbContext.Remove(entity);
		}

		public void Update<T>(T entity) where T : Entity {
			_dbContext.Update(entity);
		}

		public Task SaveAsync() {
			return _dbContext.SaveChangesAsync();
		}

		public void Save() {
			_dbContext.SaveChanges();
		}

		public void Update<T>(IEnumerable<T> entities) where T : Entity {
			_dbContext.UpdateRange(entities);
		}
	}
}