using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DishCategory: Entity
    {
        [StringLength(100)]
        public string Name { get; set; }   
    }
}