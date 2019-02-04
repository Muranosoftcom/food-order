using FoodOrder.WebUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace FoodOrder.WebUI.Areas.Admin.Controllers {
	[Area("admin")]
	public class BaseController : Controller {
		public override void OnActionExecuting(ActionExecutingContext context) {
			base.OnActionExecuting(context);
			if (!User.IsAdmin())
				context.Result = new RedirectToRouteResult(
					new RouteValueDictionary {
						{"controller", "Home"},
						{"action", "Index"}
					}
				);
		}
	}
}