using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FoodOrder.Domain.Entities {
	public class WeekDay : Entity {
		public string Name { get; set; }
		public virtual ICollection<DishItemToWeekDay> AvailableItems { get; set; }
	}
}