using System.Collections.Generic;

namespace FoodOrder.Domain.Entities {
	public class DishCategory : Entity {
		public string Name { get; set; }
		public Supplier Supplier { get; set; }
		public ICollection<DishItem> DishItems { get; set; }
	}
}