using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DishCategory: Entity
    {
        [StringLength(20)]
        public string Name { get; set; }        
    }
}