using System.ComponentModel.DataAnnotations;
using FoodOrder.Domain.Enumerations;

namespace FoodOrder.BusinessLogic.DTOs {
	public class DishDto {
		[Required] public int Id { get; set; }

		public string Name { get; set; }
		public decimal Price { get; set; }
		public int NegativeReviews { get; set; }
		public int PositiveReviews { get; set; }
		public Week[] WeekDay { get; set; }
	}
}