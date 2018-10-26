using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class WeekDay
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Name { get; set; }
        public ICollection<DishItem> AvailableItems { get; set; }
    }
}