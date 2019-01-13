using System.Threading.Tasks;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : Controller {
        private readonly IFoodService _foodService;

        public MenuController(IFoodService foodService) {
            _foodService = foodService;
        }
        
        [HttpPost]
        [Route("sync")]
        public async Task<IActionResult> Sync() {
            string syncResult = "Синхронизация успешно завершена";
            
            try {
                await _foodService.SynchronizeFood();
            }
            catch {
                syncResult = "Ошибка при выполнении синхронизации";
            }

            return Ok(new {
                syncResult
            });
        }
    }
}