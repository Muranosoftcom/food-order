namespace FoodOrder.Domain.Entities {
	public class OrderItem : Entity {
		public decimal Price { get; set; }
		public int DishItemId { get; set; }
		public DishItem DishItem { get; set; }
		public Order Order { get; set; }
	}
}