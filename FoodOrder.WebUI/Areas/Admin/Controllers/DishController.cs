using FoodOrder.BusinessLogic.Services;
using FoodOrder.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Areas.Admin.Controllers {
	[Route("admin/dish")]
	public class DishController : BaseController {
		private readonly IFoodService _foodService;
		private IFoodOrderRepository _foodOrderRepository;

		public DishController(IFoodService foodService, IFoodOrderRepository foodOrderRepository) {
			_foodService = foodService;
			_foodOrderRepository = foodOrderRepository;
		}

		public IActionResult Index() {
			return View();
		}

		[Route("sync")]
		public ActionResult Sync() {
			return View();
		}


		//        public async Task<ViewResult> SendNotification(SyncViewModel viewModel)
		//        {
		//            var usersToNotify = _repository.All<User>().Where(x => !x.IsAdmin);
		//            foreach (var user in usersToNotify)
		//            {
		//                
		//            }
		//        }
	}
}