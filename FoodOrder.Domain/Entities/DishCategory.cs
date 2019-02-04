using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Domain.Entities {
	public class DishCategory : Entity {
		public string Name { get; set; }
	}
}