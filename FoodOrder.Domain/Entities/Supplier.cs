using System.Collections.Generic;

namespace FoodOrder.Domain.Entities {
	public class Supplier : Entity {
		public string Name { get; set; }
		public bool CanMultiSelect { get; set; }
		public decimal AvailableMoneyToOrder { get; set; }
		public int Position { get; set; }
		public ICollection<DishCategory> Categories { get; set; }		
		public ICollection<DishItem> DishItems { get; set; }		
	}
}