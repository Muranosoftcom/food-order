using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrder.BusinessLogic.DTOs;
using FoodOrder.Common.Extensions;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Enumerations;
using FoodOrder.Domain.Repositories;
using FoodOrder.WebUI.Dto;
using FoodOrder.WebUI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoodOrder.WebUI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller {
        private readonly int _cafePrice;
        private readonly int _glagolPrice;
        private readonly IFoodOrderRepository _repo;

        public OrderController(IFoodOrderRepository repo, IConfiguration configuration) {
            _repo = repo;
            var configuration1 = configuration;

            int.TryParse(configuration1["OrderPrice:Glagol"], out _glagolPrice);
            int.TryParse(configuration1["OrderPrice:Cafe"], out _cafePrice);
        }

        [HttpGet]
        [Route("today-orders")]
        public UserOrderDto[] GetTodayOrder() {
            return GetWeeOrders()
                .Where(dto => dto.Day.ShortDate == DateTime.Today.Date.PrettifyDate())
                .ToArray();
        }

        [HttpGet]
        [Route("shared-today-orders")]
        public UserOrderDto[] GetSharedTodayOrder() {
            return Array.Empty<UserOrderDto>();
        }

        [HttpGet]
        [Authorize]
        [Route("week-orders")]
        public UserOrderDto[] WeekOrders() {
            Guid userId = User.GetUserId();
            
            return GetWeeOrders()
                .Where(dto => dto.User.Id == userId)
                .ToArray();
        }

        [HttpPost]
        [Authorize]
        [Route("order-lunch")]
        public async Task<IActionResult> OrderLunch([FromBody] UserOrderDto userOrder) {
            await MakeOrder(userOrder.Order.Dishes.Select(d => d.Id).ToArray());
            
            return new OkResult();
        }

        private WeekDayDto ToWeekDayDto(Order order) {
            return new WeekDayDto {
                WeekDay = order.Date.DayOfWeek.ToString(),
                UserName = $"{order.User?.UserName}",
                Suppliers = order
                    .OrderItems
                        .GroupBy(x => (x.Dish.Category.Supplier.Id, x.Dish.Category.Supplier.Name))
                        .Select(
                            x => new SupplierDto {
                                SupplierId = x.Key.Id,
                                SupplierName = x.Key.Name,
                                Categories = x.GroupBy(oi => (oi.Dish.Category.Id, oi.Dish.Category.Name))
                                    .Select(c =>
                                        new CategoryDto {
                                            Id = c.Key.Id,
                                            Name = c.Key.Name,
                                            Dishes = c.Select(d => new DishDto {
                                                Id = d.DishItemId,
                                                Name = d.Dish.Name,
                                                NegativeReviews = d.Dish.NegativeReviews,
                                                PositiveReviews = d.Dish.PositiveReviews
                                            }).ToArray()
                                        }).ToArray()
                            })
                        .OrderBy(x => x.SupplierId)
                    .ToArray()
            };
        }

        private int GetMaxSumForSupplier(Guid supplierIdValue) {
            if (supplierIdValue == SupplierType.Cafe.Id) {
                return _cafePrice;
            }
            
            if (supplierIdValue == SupplierType.Glagol.Id) {
                return _glagolPrice;
            }
            throw new KeyNotFoundException(
                    "FoodSupplier should be correlated with OrderPrice configuration in appSettings.json");
        }

        private async Task MakeOrder(Guid[] dishesIds) {
            var userId = User.GetUserId();
            var orderedItemsIds = new HashSet<Guid>(dishesIds);

            var orderedDishItems = _repo.All<Dish>().Where(x => orderedItemsIds.Contains(x.Id));


            var orderPrice = orderedDishItems.Select(x => x.Price).Sum();
            Guid? supplierId = orderedDishItems.Select(x => x.Category.Supplier.Id).FirstOrDefault();
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
                    Dish = x
                }).ToArray()
            };
            
            await _repo.InsertAsync(order);
            
            _repo.Save();
        }

        private IEnumerable<UserOrderDto> GetWeeOrders() {
            var query = _repo.All<Order>()
                .Include(x => x.User)
                .Include(x => x.OrderItems)
                    .ThenInclude(x => x.Dish)
                    .ThenInclude(x => x.Category)
                    .ThenInclude(x => x.Supplier);
            
            return query
                .ToArray()
                .Select(order => 
                    new UserOrderDto {
                        Day = new DayDto {
                            Date = order.Date
                        },
                        User = new UserDto {
                            Id = order.User.Id,
                            FullName = order.User.UserName,
                        },
                        Order = new OrderDto {
                            SupplierName = order.OrderItems
                                .Select(item => item.Dish)
                                .First().Category.Supplier.Name,
                            Dishes = order.OrderItems
                                .Select(item => item.Dish)
                                .Select(di => new DishDto {
                                    Id = di.Id,
                                    Name = di.Name,
                                    Price = di.Price,
                                    PositiveReviews = di.PositiveReviews,
                                    NegativeReviews = di.NegativeReviews
                                }).ToArray()
                        }
                    });

        }
    }
}