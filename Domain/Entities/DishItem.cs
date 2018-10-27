using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class DishItem: Entity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [Column(TypeName="Money")]
        public decimal Price { get; set; }
        public DateTime AvailableUntil { get; set; }
        public virtual ICollection<DishItemToWeekDay> AvailableOn { get; set; }
        public int PositiveReviews { get; set; }
        public int NegativeReviews { get; set; }
        
        [ForeignKey("CategoryKey")]
        public DishCategory Category { get; set; }        
        public int CategoryKey { get; set; }
        
        [ForeignKey("SupplierKey")]        
        public Supplier Supplier { get; set; }
        public int SupplierKey { get; set; }
    }
}