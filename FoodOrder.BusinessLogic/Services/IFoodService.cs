using System.Threading.Tasks;
using FoodOrder.BusinessLogic.DTOs;

namespace FoodOrder.BusinessLogic.Services {
    public interface IFoodService {
        Task SynchronizeFood();
        WeekMenuDto GetWeekMenu();
    }
}