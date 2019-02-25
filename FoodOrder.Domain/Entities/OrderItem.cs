using System;

namespace FoodOrder.Domain.Entities {
	public class OrderItem : Entity {
		public decimal Price { get; set; }
		public Guid DishItemId { get; set; }
		public Dish Dish { get; set; }
		public Order Order { get; set; }
	}
}