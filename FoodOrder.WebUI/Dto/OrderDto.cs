using FoodOrder.BusinessLogic.DTOs;

namespace FoodOrder.WebUI.Dto {
    public class OrderDto {
        public string SupplierName { get; set; }
        public DishDto[] Dishes { get; set; }
    }
}