using System;
using System.Linq;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.BusinessLogic.Services {
	public class MenuEditorService : IMenuEditorService {
		private readonly IFoodOrderRepository _foodOrderRepository;

		public MenuEditorService(IFoodOrderRepository foodOrderRepository) {
			_foodOrderRepository = foodOrderRepository;
		}

		public Supplier[] GetAllSuppliers() {
			return _foodOrderRepository.All<Supplier>()
				.Include(s => s.Categories)
					.ThenInclude(c => c.DishItems)
				.ToArray();
		}

		public void CreateSupplier(Supplier supplier) {
			_foodOrderRepository.Insert(supplier);
			_foodOrderRepository.Save();
		}

		public void UpdateSupplier(Supplier supplier) {
			_foodOrderRepository.Update(supplier);
			_foodOrderRepository.Save();
		}

		public void DeleteSupplier(Guid supplierId) {
			if (supplierId.Equals(Guid.Empty)) {
				return;
			}
			
			_foodOrderRepository.Delete(new Supplier { Id = supplierId });
			_foodOrderRepository.Save();
		}

		public void CreateCategory(DishCategory category, Guid supplierId) {
			if (supplierId.Equals(Guid.Empty)) {
				throw new ArgumentException($"{nameof(supplierId)} can't be empty");
			}
			
			var supplier = _foodOrderRepository.GetById<Supplier>(supplierId) ?? new Supplier();
			category.Supplier = supplier;
			
			_foodOrderRepository.Insert(category);
			_foodOrderRepository.Save();
		}

		public void UpdateCategory(DishCategory category) {
			_foodOrderRepository.Update(category);
			_foodOrderRepository.Save();
		}

		public void DeleteCategory(Guid categoryId) {
			if (categoryId.Equals(Guid.Empty)) {
				throw new ArgumentException($"{nameof(categoryId)} can't be empty");
			}
			
			_foodOrderRepository.Delete(new DishCategory { Id = categoryId });
			_foodOrderRepository.Save();
		}

		public void CreateDish(Dish dish, Guid categoryId) {
			if (categoryId.Equals(Guid.Empty)) {
				throw new ArgumentException($"{nameof(categoryId)} can't be empty");
			}
		
			var category = _foodOrderRepository.GetById<DishCategory>(categoryId) ?? new DishCategory();
			dish.Category = category;

			_foodOrderRepository.Insert(dish);
			_foodOrderRepository.Save();
		}

		public void UpdateDish(Dish dish) {
			_foodOrderRepository.Update(dish);
			_foodOrderRepository.Save();
		}

		public void DeleteDish(Guid dishId) {
			if (dishId.Equals(Guid.Empty)) {
				throw new ArgumentException($"{nameof(dishId)} can't be empty");
			}
			
			_foodOrderRepository.Delete(new Dish { Id = dishId });
			_foodOrderRepository.Save();
		}
	}
}