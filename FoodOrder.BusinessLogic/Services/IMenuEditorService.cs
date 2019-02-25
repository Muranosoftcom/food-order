using System;
using FoodOrder.Domain.Entities;

namespace FoodOrder.BusinessLogic.Services {
	public interface IMenuEditorService {
		Supplier[] GetAllSuppliers();
		void UpdateSupplier(Supplier supplier);
		void DeleteSupplier(Guid supplierId);
		void CreateSupplier(Supplier supplier);
		void CreateCategory(DishCategory name, Guid supplierId);
		void UpdateCategory(DishCategory category);
		void DeleteCategory(Guid categoryId);
		void CreateDish(Dish dish, Guid categoryId);
		void UpdateDish(Dish dish);
		void DeleteDish(Guid id);
	}
}