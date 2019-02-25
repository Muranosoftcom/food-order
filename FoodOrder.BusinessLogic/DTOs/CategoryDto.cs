using System;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.BusinessLogic.DTOs {
	public class CategoryDto {
		[Required] public Guid Id { get; set; }

		public string Name { get; set; }

		public int Position { get; set; }
		
		public Guid SupplierId { get; set; }
		
		public DishDto[] Dishes { get; set; }
	}
}