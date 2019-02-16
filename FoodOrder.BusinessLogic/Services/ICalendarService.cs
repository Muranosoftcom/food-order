using System.Collections.Generic;
using FoodOrder.Domain.Entities;

namespace FoodOrder.BusinessLogic.Services {
	public interface ICalendarService {
		IEnumerable<Day> GetAllHolidays();
	}
}