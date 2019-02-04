using FoodOrder.BusinessLogic.DTOs;

namespace FoodOrder.WebUI.Dto {
    public class DayMenuDto {
        public string ShortDate { get; set; }
        public SupplierDto[] Suppliers { get; set; }
    }
}