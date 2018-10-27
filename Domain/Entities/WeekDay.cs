using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class WeekDay: Entity
    {        
        [MaxLength(15)]
        public string Name { get; set; }
        public virtual ICollection<DishItemToWeekDay> AvailableItems { get; set; }
    }
}