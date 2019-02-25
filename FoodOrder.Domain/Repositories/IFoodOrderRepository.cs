using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrder.Domain.Entities;

namespace FoodOrder.Domain.Repositories {
	public interface IFoodOrderRepository {
		IQueryable<T> All<T>() where T : Entity;
		T GetById<T>(Guid id) where T : Entity;
		void Insert<T>(T entity) where T : Entity;
		Task InsertAsync<T>(T entity) where T : Entity;
		Task InsertAsync<T>(IEnumerable<T> entities) where T : Entity;
		void Delete<T>(T entity) where T : Entity;
		void Update<T>(T entity) where T : Entity;
		Task SaveAsync();
		void Save();
	}
}