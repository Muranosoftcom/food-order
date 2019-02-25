using System.Linq;
using FoodOrder.Domain.Entities;

namespace FoodOrder.Domain.Repositories {
	public interface ICalendarRepository {
		IQueryable<Day> All();
	}
}