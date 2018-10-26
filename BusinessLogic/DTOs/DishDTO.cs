using Core;

namespace BusinessLogic.DTOs
{
    public class DishDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Week[] WeekDay { get; set; }
    }
}