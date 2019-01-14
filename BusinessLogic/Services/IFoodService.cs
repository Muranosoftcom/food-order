using System.Threading.Tasks;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services {
    public interface IFoodService {
        Task SynchronizeFood();
        WeekMenuDto GetWeekMenu();
    }
}