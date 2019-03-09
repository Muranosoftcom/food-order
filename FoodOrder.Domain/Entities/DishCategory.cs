using System;
using System.Collections.Generic;

namespace FoodOrder.Domain.Entities {
	public class DishCategory : Entity {
		public DishCategory() {
			DishItems = new HashSet<Dish>();
		}
		public string Name { get; set; }
		public int Position { get; set; }
		public Guid SupplierId { get; set; }
		public Supplier Supplier { get; set; }
		public ICollection<Dish> DishItems { get; set; }
	}
}