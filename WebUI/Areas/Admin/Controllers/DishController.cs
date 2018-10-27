using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    
    public class DishController : BaseController {
        private readonly IFoodService _foodService;

        public DishController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Dish/Create
        public ActionResult Sync()
        {
            return View();
        }

        // POST: Dish/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ViewResult> Sync(SyncViewModel viewModel)
        {
            try
            {
                await _foodService.SynchronizeFood();
                return View(new SyncViewModel
                {
                    SyncResult = "Синхронизация успешно завершена"
                });
            }
            catch
            {
                return View(new SyncViewModel {
                    SyncResult = "Ошибка при выполнении синхронизации"
                });
            }
        }
    }
}