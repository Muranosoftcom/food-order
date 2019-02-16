using System;

namespace FoodOrder.Domain.Entities {
	public class Day {
		public DateTime Date { get; set; }
		public bool IsHoliday { get; set; }

		public static Day Create(string shortDate, bool isHoliday = false) {
			return new Day {
				Date = Convert.ToDateTime(shortDate),
				IsHoliday = isHoliday,
			};
		}
	}
}