using Core;

namespace BusinessLogic.DTOs
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Week[] WeekDay { get; set; }
    }
}