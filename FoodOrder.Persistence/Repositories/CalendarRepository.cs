using System.Linq;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;

namespace FoodOrder.Persistence.Repositories {
	public class CalendarRepository : ICalendarRepository {
		private readonly IFoodOrderDbContext _dbContext;

		public CalendarRepository(IFoodOrderDbContext dbContext) {
			_dbContext = dbContext;
		}
		
		public IQueryable<Day> All() {
			return _dbContext.Calendar;
		}
	}
}