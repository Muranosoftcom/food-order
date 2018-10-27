using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;

namespace WebUI.Controllers
{
    [Route("api/order")]
    [ApiController]
//    [Authorize]
    public class OrderApiController : Controller
    {
        private IRepository _repo;

        public OrderApiController(IRepository repo)
        {
            _repo = repo;
        }
//
//        [HttpGet]
//        [Route("get-day-menu")]
//        public ActionResult GetDayMenu (DateTime date) {
//            var now = DateTime.UtcNow;
//
//            var order = _repo.All<Order>().SingleOrDefault(x => x.Date == date);
//            var items = _repo.All<OrderItem>().Where(x => x.OrderKey == order.Id).ToArray();
//
//            List<(string dayName, DishItem dish)> dayNameDishPairs = new List<(string, DishItem)>();
//            foreach (var dish in order.OrderItems) {
//                var dayNames = dish.AvailableOn.Select(x => x.WeekDay.Name).ToArray();
//                foreach (var dayName in dayNames) {
//                    dayNameDishPairs.Add((dayName: dayName, dish: dish));
//                }
//            }
//
//            var dishesByDayName = dayNameDishPairs.ToLookup(x => x.dayName, x => x.dish);
//
//            var dto = new WeekMenuDto {
//                WeekDays = dishesByDayName.Select(x => new WeekDayDto {
//                    WeekDay = x.Key,
//                    Suppliers = x.GroupBy(d => (id: d.Supplier.Id, name: d.Supplier.Name)).Select(d => {
//                        var dishItemByPair =
//                            d.ToDictionary(di => (categoryId: di.Category.Id, categoryName: di.Category.Name),
//                                di => di);
//                        return new SupplierDto {
//                            SupplierId = d.Key.id,
//                            SupplierName = d.Key.name,
//                            Categories = d.Select(dishSupplierPair => new CategoryDto {
//                                Id = dishSupplierPair.Category.Id,
//                                Name = dishSupplierPair.Category.Name,
//                                Dishes = dishItemByPair.Select(y => new DishDto {
//                                    Id = y.Value.Id,
//                                    Name = y.Value.Name,
//                                    Price = y.Value.Price
//                                }).ToArray()
//                            }).ToArray()
//                        };
//                    }).ToArray()
//                }).ToArray()
//            };
//
//            return new JsonResult(dto);
//        }

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
        [HttpGet]
        [Route("get-week-menu")]
        public ActionResult<WeekMenuDto> GetWeekMenu()
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

        [HttpPost]
        [Route("post-order")]
        public async Task<ActionResult> PostOrder([FromBody] SupplierDto supplierDto)
        {
                var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value);

            DishDto[] orderedItemsDtos = supplierDto.Categories.SelectMany(x => x.Dishes).ToArray();
            HashSet<int> orderedItemsIds = new HashSet<int>(orderedItemsDtos.Select(x => x.Id));
            IQueryable<DishItem> orderedItems = _repo.All<DishItem>().Where(x => orderedItemsIds.Contains(x.Id));

            decimal orderPrice = orderedItems.Select(x => x.Price).Sum();
            User user = _repo.GetById<User>(userId);
            var order = new Order
            {
                Date = DateTime.Now,
                Price = orderPrice,
                User = user
            };
            var orderItems = orderedItems.Select(x => new OrderItem
            {
                Price = x.Price,
                DishItem = x,
                Order = order
            });
            await _repo.InsertAsync(orderedItems);
            return new OkResult();
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