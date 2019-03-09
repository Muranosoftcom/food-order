using FoodOrder.BusinessLogic.DTOs;

namespace FoodOrder.BusinessLogic.Services {
	public interface IWeekMenuService {
		WeekDayDto[] GetWeekDaysMenu();
	}
}