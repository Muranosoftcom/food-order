using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.Domain.Entities {
	public class Order : Entity {
		public DateTime Date { get; set; }
		public decimal Price { get; set; }
		public User User { get; set; }
		public virtual ICollection<OrderItem> OrderItems { get; set; }
	}
}