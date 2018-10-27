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
using WebUI.Infrastructure;

namespace WebUI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderApiController : Controller
    {
        private IRepository _repo;

        public OrderApiController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Authorize]
        [Route("get-week-menu")]
        public ActionResult<WeekMenuDto> GetWeekMenu()
        {
            var now = DateTime.UtcNow;

            var availableDishes = _repo.All<DishItem>()
                .Include(x => x.AvailableOn)
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Where(x => x.AvailableUntil >= now);             

            List<(string dayName, DishItem dish)> dayNameDishPairs = new List<(string, DishItem)>();
            foreach (var dish in availableDishes)
            {
                var dayIds = new HashSet<int>(dish.AvailableOn.Select(x => x.WeekDayId));
                var dayNames = _repo.All<WeekDay>().Where(x => dayIds.Contains(x.Id)).Select(x => x.Name);
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
                        var dishItemByPairPairs =
                            d.Select(di => (key: (categoryId: di.Category.Id, categoryName: di.Category.Name),
                                value: di));
                        var dishItemByPair = dishItemByPairPairs.ToLookup(y => y.key, y => y.value);
                        return new SupplierDto
                        {
                            SupplierId = d.Key.id,
                            SupplierName = d.Key.name,
                            Categories = dishItemByPair.Select(z => new CategoryDto
                            {
                                Id = z.Key.categoryId,
                                Name = z.Key.categoryName,
                                Dishes = z.Select(f => new DishDto
                                {
                                    Id = f.Id,
                                    Name = f.Name,
                                    Price = f.Price
                                }).ToArray()
                            }).ToArray()
                        };
                    }).ToArray()
                }).ToArray()
            };

            return new JsonResult(dto);
        }

        [HttpPost]
        [Authorize]
        [Route("post-order")]
        public async Task<ActionResult> PostOrder([FromBody] SupplierDto supplierDto)
        {
            var userId = User.GetUserId().Value;
            DishDto[] orderedItemsDtos = supplierDto.Categories.SelectMany(x => x.Dishes).ToArray();
            HashSet<int> orderedItemsIds = new HashSet<int>(orderedItemsDtos.Select(x => x.Id));
            IQueryable<DishItem> orderedDishItems = _repo.All<DishItem>().Where(x => orderedItemsIds.Contains(x.Id));

            decimal orderPrice = orderedDishItems.Select(x => x.Price).Sum();
            User user = _repo.GetById<User>(userId);
            var order = new Order
            {
                Date = DateTime.Now,
                Price = orderPrice,
                User = user,
                OrderItems = orderedDishItems.Select(x => new OrderItem
                {
                    Price = x.Price,
                    DishItem = x
                }).ToArray()
            };
            await _repo.InsertAsync(order);
            _repo.Save();
            return new OkResult();
        }


        [HttpGet]
        [Route("get-today-order")]
        public ActionResult<WeekMenuDto> GetTodayOrder()
        {
            Order[] orders;
            if (!HttpContext.User?.Identity.IsAuthenticated ?? false)
            {
                orders = _repo.All<Order>().Where(x => x.Date == DateTime.Today).ToArray();
            }
            else
            {
                orders = _repo.All<Order>().Where(x => x.UserId == User.GetUserId().Value && x.Date == DateTime.Today).ToArray();
            }

            return new WeekMenuDto {WeekDays = orders.Select(ToWeekDayDto).ToArray()};
        }

        private WeekDayDto ToWeekDayDto(Order order)
        {
            return new WeekDayDto
            {
                WeekDay = order.Date.DayOfWeek.ToString(),
                UserName = $"{order.User.FirstName} {order.User.LastName}",
                Suppliers = order.OrderItems.Select(x => new SupplierDto
                {
                    SupplierId = x.DishItem.Supplier.Id,
                    SupplierName = x.DishItem.Supplier.Name,
                    Categories = order.OrderItems.GroupBy(oi => oi.DishItem.Category).Select(c => new CategoryDto
                    {
                        Id = c.Key.Id,
                        Name = c.Key.Name,
                        Dishes = c.Select(d => new DishDto
                        {
                            Id = d.DishItemId,
                            Name = d.DishItem.Name
                        }).ToArray()
                    }).ToArray()
                }).ToArray()
            };
        }
    }
}