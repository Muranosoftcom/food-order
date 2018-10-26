using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class WeekDay
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(15)]
        public string Name { get; set; }
        public ICollection<DishItemToWeekDay> AvailableItems { get; set; }
    }
}