using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.Services;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    
    [Route("admin/dish")]
    public class DishController : BaseController
    {
        private IRepository _repository;
        private readonly IFoodService _foodService;

        public DishController(IFoodService foodService, IRepository repository)
        {
            _foodService = foodService;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("sync")]
        public ActionResult Sync()
        {
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