using System.ComponentModel.DataAnnotations;
using Core;

namespace BusinessLogic.DTOs
{
    public class DishDto
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int NegativeRewievs { get; set; }
        public int PositiveRewievs { get; set; }
        public Week[] WeekDay { get; set; }
    }
}