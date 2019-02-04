using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Domain.Entities {
	public class DishCategory : Entity {
		[StringLength(100)] public string Name { get; set; }
	}
}