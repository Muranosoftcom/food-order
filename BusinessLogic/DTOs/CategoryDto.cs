namespace BusinessLogic.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DishDto[] Dishes { get; set; }
    }
}