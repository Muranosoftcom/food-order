using Microsoft.AspNetCore.Mvc;

namespace FoodOrder.WebUI.Controllers {
    public class HomeController : Controller {
        public IActionResult Error() {
            return View();
        }
    }
}