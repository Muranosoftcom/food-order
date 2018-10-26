namespace BusinessLogic.DTOs
{
    public class FoodDTO
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public CategoryDTO[] Categories { get; set; }
    }

    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DishDTO[] Dishes { get; set; }
    }
}