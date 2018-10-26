using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }       
    }
}