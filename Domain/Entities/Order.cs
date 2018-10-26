using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Order: Entity
    {        
        public DateTime Date { get; set; }
        
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}