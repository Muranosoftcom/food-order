using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Supplier: Entity
    {

        [StringLength(20)]
        public string Name { get; set; }       
    }
}