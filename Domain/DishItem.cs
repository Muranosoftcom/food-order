using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class DishItem
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Column(TypeName="Money")]
        public decimal Price { get; set; }
        public DateTime AvailableUntil { get; set; }
        public ICollection<WeekDay> AvailableOn { get; set; }
        public int PositiveReviews { get; set; }
        public int NegativeReviews { get; set; }
    }
}