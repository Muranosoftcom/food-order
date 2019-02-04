namespace FoodOrder.WebUI.Dto {
    public class UserOrderDto {
        public UserDto User { get; set; }
        public DayDto Day { get; set; }
        public OrderDto Order { get; set; }
    }
}