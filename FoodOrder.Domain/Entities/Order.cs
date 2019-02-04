using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.Domain.Entities {
	public class Order : Entity {
		public DateTime Date { get; set; }

		[Column(TypeName = "Money")] public decimal Price { get; set; }

		[ForeignKey("UserId")] public User User { get; set; }

		public int UserId { get; set; }

		public virtual ICollection<OrderItem> OrderItems { get; set; }
	}
}