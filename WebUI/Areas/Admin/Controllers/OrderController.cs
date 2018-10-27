using System;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IRepository _repository;

        public OrderController(IRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Statistic()
        {
            var orders = _repository.All<Order>().Where(x => x.Price > 10 && x.Date.Month == DateTime.Today.AddMonths(-1).Month)
                .Include(x => x.User).OrderBy(x => x.User.FirstName).ToArray();

            var models = orders.Select(x => new OverpriceViewModel
            {
                UserName = x.User.FirstName,
                Price = x.Price,
                PriceDifference = x.Price - 10,
                Date = x.Date,
            }).ToArray();
        
            return View(models);
        }
    }
}