using System;
using System.Collections.Generic;
using System.Linq;
using FoodOrder.BusinessLogic.DTOs;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.BusinessLogic.Services {
	public class WeekMenuService: IWeekMenuService {
        private readonly IFoodOrderRepository _repository;

        public WeekMenuService(IFoodOrderRepository repository) {
            _repository = repository;
        }
        
		public WeekDayDto[] GetWeekDaysMenu() {
            var availableDishes = _repository.All<Dish>()
                .Include(x => x.Category)
                    .ThenInclude(category => category.Supplier)
                .Where(dish => dish.IsAvailable);

            List<(DayOfWeek dayOfWeek, Dish dish)> dayNameDishPairs = new List<(DayOfWeek, Dish)>();
            
            foreach (var dish in availableDishes) {
                DayOfWeek[] dayOfWeeks = dish.AvailableAt;
                
                foreach (var dayOfWeek in dayOfWeeks) {
                    dayNameDishPairs.Add((dayOfWeek: dayOfWeek, dish: dish));
                }
            }

            var dishesByDayName = dayNameDishPairs.ToLookup(x => x.dayOfWeek, x => x.dish);
            
            return dishesByDayName.Select(dishByDayName => new WeekDayDto {
                DayOfWeek = dishByDayName.Key,
                WeekDay = dishByDayName.Key.ToString(),
                Suppliers = dishByDayName.GroupBy(dish => (id: dish.Category.SupplierId, name: dish.Category.Supplier.Name, canMultiSelect: dish.Category.Supplier.CanMultiSelect, availableMoneyToOrder: dish.Category.Supplier.AvailableMoneyToOrder ))
                    .Select(d => {
                        var dishItemByPairPairs = d.Select(di => (key: (categoryId: di.Category.Id, categoryName: di.Category.Name), value: di));
                        var dishItemByPair = dishItemByPairPairs.ToLookup(y => y.key, y => y.value);
                            
                        return new SupplierDto {
                            SupplierId = d.Key.id,
                            SupplierName = d.Key.name,
                            CanMultiSelect = d.Key.canMultiSelect,
                            AvailableMoneyToOrder = d.Key.availableMoneyToOrder,
                            Categories = dishItemByPair.Select(z => new CategoryDto {
                                Id = z.Key.categoryId,
                                Name = z.Key.categoryName,
                                Dishes = z.Select(f => new DishDto {
                                    Id = f.Id,
                                    Name = f.Name,
                                    Price = f.Price,
                                    NegativeReviews = f.NegativeReviews,
                                    PositiveReviews = f.PositiveReviews
                                }).ToArray()
                            }).ToArray()
                        };
                    }).OrderBy(t => t.SupplierId).ToArray()
            }).ToArray();
		}
	}
}