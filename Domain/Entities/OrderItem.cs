using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }        
        [Column(TypeName="Money")]
        public decimal Price { get; set; }
        public DateTime OrderedAt { get; set; }
        
        [ForeignKey("DishItemId")]
        public DishItem DishItem { get; set; }
        public int DishItemId { get; set; }
            
    }
}