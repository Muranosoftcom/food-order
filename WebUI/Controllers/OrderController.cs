using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using Core;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebUI.Infrastructure;

namespace WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller {
        private readonly int _cafePrice;
        private readonly int _glagolPrice;
        private readonly IRepository _repo;

        public OrderController(IRepository repo, IConfiguration configuration) {
            _repo = repo;
            var configuration1 = configuration;

            int.TryParse(configuration1["OrderPrice:Glagol"], out _glagolPrice);
            int.TryParse(configuration1["OrderPrice:Cafe"], out _cafePrice);
        }


        [HttpPost]
        [Authorize]
        [Route("post-order")]
        public async Task<ActionResult> PostOrder([FromBody] int[] dishesIds) {
            var userId = User.GetUserId().Value;
            var orderedItemsIds = new HashSet<int>(dishesIds);

            var orderedDishItems = _repo.All<DishItem>().Where(x => orderedItemsIds.Contains(x.Id));


            var orderPrice = orderedDishItems.Select(x => x.Price).Sum();
            int? supplierId = orderedDishItems.Select(x => x.Supplier.Id).FirstOrDefault();
            if (supplierId.HasValue) {
                var maxPrice = GetMaxSumForSupplier(supplierId.Value);
                orderPrice = Math.Max(maxPrice, orderPrice);
            }

            var user = _repo.GetById<User>(userId);
            var order = new Order {
                Date = DateTime.Now,
                Price = orderPrice,
                User = user,
                OrderItems = orderedDishItems.Select(x => new OrderItem {
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
        public ActionResult<WeekMenuDto> GetTodayOrder() {
            var query = _repo.All<Order>().Include(x => x.User)
                .Include(x => x.OrderItems).ThenInclude(x => x.DishItem).ThenInclude(x => x.Category)
                .Include(x => x.OrderItems).ThenInclude(x => x.DishItem).ThenInclude(x => x.Supplier);
            var orders = !User.IsAuthenticated()
                ? query.Where(x => x.Date.Date == DateTime.Today.Date).ToArray()
                : query.Where(x => x.UserId == User.GetUserId().Value && x.Date.Date == DateTime.Today.Date).ToArray();
            return new WeekMenuDto {WeekDays = orders.Select(ToWeekDayDto).ToArray()};
        }

        [HttpPut]
        [Route("increment-rating")]
        [Authorize]
        public async Task<ActionResult> IncrementRating(int dishItemId) {
            var dishItem = _repo.GetById<DishItem>(dishItemId);
            dishItem.PositiveReviews++;
            _repo.Update(dishItem);
            await _repo.SaveAsync();
            return new OkResult();
        }


        [HttpPut]
        [Route("decrement-rating")]
        [Authorize]
        public async Task<ActionResult> DecrementRating(int dishItemId) {
            var dishItem = _repo.GetById<DishItem>(dishItemId);
            dishItem.NegativeReviews++;
            _repo.Update(dishItem);
            await _repo.SaveAsync();
            return new OkResult();
        }

        private WeekDayDto ToWeekDayDto(Order order) {
            return new WeekDayDto {
                WeekDay = order.Date.DayOfWeek.ToString(),
                UserName = $"{order.User?.UserName} {order.User?.UserName}",
                Suppliers = order.OrderItems.GroupBy(x => (x.DishItem.Supplier.Id, x.DishItem.Supplier.Name)).Select(
                    x => new SupplierDto {
                        SupplierId = x.Key.Id,
                        SupplierName = x.Key.Name,
                        Categories = x.GroupBy(oi => (oi.DishItem.Category.Id, oi.DishItem.Category.Name)).Select(c =>
                            new CategoryDto {
                                Id = c.Key.Id,
                                Name = c.Key.Name,
                                Dishes = c.Select(d => new DishDto {
                                    Id = d.DishItemId,
                                    Name = d.DishItem.Name,
                                    NegativeRewievs = d.DishItem.NegativeReviews,
                                    PositiveRewievs = d.DishItem.PositiveReviews
                                }).ToArray()
                            }).ToArray()
                    }).OrderBy(x => x.SupplierId).ToArray()
            };
        }

        private int GetMaxSumForSupplier(int supplierIdValue) {
            switch (supplierIdValue) {
                case (int) FoodSupplier.Cafe: {
                    return _cafePrice;
                }
                case (int) FoodSupplier.Glagol: {
                    return _glagolPrice;
                }
                default:
                    throw new KeyNotFoundException(
                        "FoodSupplier should be correlated with OrderPrice configuration in appSettings.json");
            }
        }
    }
}