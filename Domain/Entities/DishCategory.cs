using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DishCategory: Entity
    {
        [StringLength(100)]
        public string Name { get; set; }   
    }
}