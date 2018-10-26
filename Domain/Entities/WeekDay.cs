using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class WeekDay
    {        
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Name { get; set; }
        public ICollection<DishItemToWeekDay> AvailableItems { get; set; }
    }
}