using System.Collections.Generic;
using System.Linq;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;

namespace FoodOrder.BusinessLogic.Services {
	public class CalendarService: ICalendarService {
		private readonly ICalendarRepository _repository;

		public CalendarService(ICalendarRepository repository) {
			_repository = repository;
		}

		public IEnumerable<Day> GetAllHolidays() {
			return _repository.All().ToArray();
		}
	}
}