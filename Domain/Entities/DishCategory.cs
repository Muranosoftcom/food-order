using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class DishCategory
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }        
    }
}