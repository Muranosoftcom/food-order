using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.BusinessLogic.DTOs {
	public class DishDto {
		[Required] public Guid Id { get; set; }

		public string Name { get; set; }
		public decimal Price { get; set; }
		public int NegativeReviews { get; set; }
		public int PositiveReviews { get; set; }
		public Guid CategoryId { get; set; }
		public ICollection<int> AvailableAt { get; set; }
	}
}