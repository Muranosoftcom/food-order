using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Areas.Admin.Controllers {
	[Route("admin/rating")]
	public class RatingController : BaseController {
		public IActionResult Index() {
			return View();
		}
	}
}