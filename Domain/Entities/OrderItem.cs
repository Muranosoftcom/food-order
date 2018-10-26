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
        
        [ForeignKey("DishItemId")]
        public DishItem DishItem { get; set; }
        public int DishItemId { get; set; }
            
        [ForeignKey("OrderKey")]
        public Order Order { get; set;}
        public int OrderKey { get; set; }        
    }
}