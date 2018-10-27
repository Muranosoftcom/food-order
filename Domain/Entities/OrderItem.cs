using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class OrderItem: Entity
    {
      
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