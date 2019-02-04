using System.ComponentModel.DataAnnotations;

namespace FoodOrder.BusinessLogic.DTOs {
	public class CategoryDto {
		[Required] public int Id { get; set; }

		public string Name { get; set; }

		[Required] public DishDto[] Dishes { get; set; }
	}
}