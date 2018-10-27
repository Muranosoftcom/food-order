using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.DTOs;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebUI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller
    {
        private IRepository _repo;

        public OrderController(IRepository repo)
        {
            _repo = repo;
        }

//        [HttpGet]
//        [Route("get-day-menu")]
//        public ActionResult GetDayMenu (DateTime date) {
//            var now = DateTime.UtcNow;
//
//            var order = _repo.All<Order>().SingleOrDefault(x => x.Date == date);
//                          var items = _repo.All<OrderItem>().Where(x => x.OrderKey == order.Id).ToArray();
//              
//                          List<(string dayName, DishItem dish)> dayNameDishPairs = new List<(string, DishItem)>();
//                          foreach (var dish in order) {
//                              var dayNames = dish.AvailableOn.Select(x => x.WeekDay.Name).ToArray();
//                              foreach (var dayName in dayNames) {
//                                  dayNameDishPairs.Add((dayName: dayName, dish: dish));
//                              }
//                          }
//              
//                          var dishesByDayName = dayNameDishPairs.ToLookup(x => x.dayName, x => x.dish);
//              
//                          var dto = new WeekMenuDto {
//                              WeekDays = dishesByDayName.Select(x => new WeekDayDto {
//                                  WeekDay = x.Key,
//                                  Suppliers = x.GroupBy(d => (id: d.Supplier.Id, name: d.Supplier.Name)).Select(d => {
//                                      var dishItemByPair =
//                                          d.ToDictionary(di => (categoryId: di.Category.Id, categoryName: di.Category.Name),
//                                              di => di);
//                                      return new SupplierDto {
//                                          SupplierId = d.Key.id,
//                                          SupplierName = d.Key.name,
//                                          Categories = d.Select(dishSupplierPair => new CategoryDto {
//                                              Id = dishSupplierPair.Category.Id,
//                                              Name = dishSupplierPair.Category.Name,
//                                              Dishes = dishItemByPair.Select(y => new DishDto {
//                                                  Id = y.Value.Id,
//                                                  Name = y.Value.Name,
//                                                  Price = y.Value.Price
//                                              }).ToArray()
//                                          }).ToArray()
//                                      };
//                                  }).ToArray()
//                              }).ToArray()
//                          };
//
//            return new JsonResult(dto);
//        }

        [HttpGet]   
        [Route("get-week-menu")]
        public ActionResult GetWeekMenu()
        {
            var now = DateTime.UtcNow;

            var availableDishes = _repo.All<DishItem>().Include(x => x.AvailableOn).Where(x => x.AvailableUntil > now)
                .ToArray();

            List<(string dayName, DishItem dish)> dayNameDishPairs = new List<(string, DishItem)>();
            foreach (var dish in availableDishes)
            {
                var dayNames = dish.AvailableOn.Select(x => x.WeekDay.Name).ToArray();
                foreach (var dayName in dayNames)
                {
                    dayNameDishPairs.Add((dayName: dayName, dish: dish));
                }
            }

            var dishesByDayName = dayNameDishPairs.ToLookup(x => x.dayName, x => x.dish);

            var dto = new WeekMenuDto
            {
                WeekDays = dishesByDayName.Select(x => new WeekDayDto
                {
                    WeekDay = x.Key,
                    Suppliers = x.GroupBy(d => (id: d.Supplier.Id, name: d.Supplier.Name)).Select(d =>
                    {
                        var dishItemByPair =
                            d.ToDictionary(di => (categoryId: di.Category.Id, categoryName: di.Category.Name),
                                di => di);
                        return new SupplierDto
                        {
                            SupplierId = d.Key.id,
                            SupplierName = d.Key.name,
                            Categories = d.Select(dishSupplierPair => new CategoryDto
                            {
                                Id = dishSupplierPair.Category.Id,
                                Name = dishSupplierPair.Category.Name,
                                Dishes = dishItemByPair.Select(y => new DishDto
                                {
                                    Id = y.Value.Id,
                                    Name = y.Value.Name,
                                    Price = y.Value.Price
                                }).ToArray()
                            }).ToArray()
                        };
                    }).ToArray()
                }).ToArray()
            };

            return new JsonResult(dto);
        }

       



        //        [HttpGet("{id}", Name = "GetTodo")]
        //        public ActionResult<TodoItem> GetById(long id)
        //        {
        //            var item = _context.TodoItems.Find(id);
        //            if (item == null)
        //            {
        //                return NotFound();
        //            }
        //
        //            return item;
        //        }
    }
}