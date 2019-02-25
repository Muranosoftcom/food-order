using System;
using System.Linq;

namespace FoodOrder.Domain.Entities {
	public class Dish : Entity {
		
		public string Name { get; set; }
		public decimal Price { get; set; }
		public int PositiveReviews { get; set; }
		public int NegativeReviews { get; set; }
		public DishCategory Category { get; set; }
		/// <summary>
		/// Used for store AvailableAt array in serialized form
		/// </summary>
		public string InternalDayOfWeekArray { get; set; }
		public DayOfWeek[] AvailableAt { 
			get {
				return string.IsNullOrEmpty(InternalDayOfWeekArray) 
					? Array.Empty<DayOfWeek>() 
					: InternalDayOfWeekArray.Split(';').Select(val => (DayOfWeek)int.Parse(val)).ToArray();
			}
			
			set {
				InternalDayOfWeekArray = string.Join(";", value.Cast<int>());
			} 
		}
		public bool IsAvailable => AvailableAt.Length > 0;
	}
}