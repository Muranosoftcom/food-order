namespace FoodOrder.Domain.Entities {
	public class DishItemToWeekDay {
		public DishItem DishItem { get; set; }
		public int DishItemId { get; set; }

		public WeekDay WeekDay { get; set; }
		public int WeekDayId { get; set; }
	}
}