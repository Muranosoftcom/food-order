using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodOrder.Domain.Entities {
	public class DishItem : Entity {
		public string Name { get; set; }
		public decimal Price { get; set; }
		public DateTime AvailableUntil { get; set; }
		public virtual ICollection<DishItemToWeekDay> AvailableOn { get; set; }
		public int PositiveReviews { get; set; }
		public int NegativeReviews { get; set; }

		public DishCategory Category { get; set; }
		public Supplier Supplier { get; set; }
		public int SupplierId { get; set; }
	}
}