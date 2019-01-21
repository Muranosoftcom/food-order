namespace BusinessLogic.DTOs {
    public class OrderDto {
        public string SupplierName { get; set; }
        public DishDto[] Dishes { get; set; }
    }
}