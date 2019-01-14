using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.SpreadsheetParsing;
using Core;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SpreadsheetIntegration;
using SpreadsheetIntegration.Core;

namespace BusinessLogic.Services {
    public class FoodService : IFoodService {
        private readonly IRepository _repo;
        private readonly IAsyncSpreadsheetProvider _spreadsheetProvider;

        public FoodService(IRepository repo, IAsyncSpreadsheetProvider spreadsheetProvider) {
            _repo = repo;
            _spreadsheetProvider = spreadsheetProvider;
        }

        public async Task SynchronizeFood() {
            var completionSource = new TaskCompletionSource<Unit>();
            var glagolFood = _spreadsheetProvider.GetAsync(
                "1ik4DnYQL3hCbxF4PRmmeaGhcTXYW4BcCbGvtijoK_rk",
                new SpreadsheetGetRequest("Menu", "A2:F30"), CancellationToken.None).ToObservable();

            var kafeFood = _spreadsheetProvider.GetAsync(
                "1sbbFJqa-X4KW91EmPyO9NTbP8bbuwg3szCSC3FTeR2c",
                new SpreadsheetGetRequest("Menu", "A2:E100"), CancellationToken.None).ToObservable();

            var glagolFoodDto = glagolFood.Select(x => ParsingRegistry.GetParser(FoodSupplier.Glagol).ExtractFood(x))
                .Select(x => {
                    var food = new SupplierDto {
                        SupplierId = (int) FoodSupplier.Glagol,
                        SupplierName = "ГлаголЪ",
                        Categories = x.GroupBy(g => g.Category).Select(c => new CategoryDto {
                            Name = c.Key,
                            Dishes = c.GroupBy(g => (g.Name, g.Price)).Select(d => new DishDto {
                                Name = d.Key.Item1,
                                Price = d.Key.Item2,
                                WeekDay = d.Select(g => g.Day).ToArray()
                            }).ToArray()
                        }).ToArray()
                    };

                    return food;
                });

            var kafeFoodDto = kafeFood.Select(x => ParsingRegistry.GetParser(FoodSupplier.Cafe).ExtractFood(x)).Select(
                x => {
                    var food = new SupplierDto {
                        SupplierId = (int) FoodSupplier.Cafe,
                        SupplierName = "Столовая",
                        Categories = x.GroupBy(g => g.Category).Select(c => new CategoryDto {
                            Name = c.Key,
                            Dishes = c.GroupBy(g => (g.Name, g.Price)).Select(d => new DishDto {
                                Name = d.Key.Item1,
                                Price = d.Key.Item2,
                                WeekDay = d.Select(g => g.Day).ToArray()
                            }).ToArray()
                        }).ToArray()
                    };

                    return food;
                });

            glagolFoodDto.Merge(kafeFoodDto).Aggregate(new List<SupplierDto>(), (x, y) => {
                x.Add(y);
                return x;
            }).Subscribe(async x => await SaveChanges(x, completionSource));
            await completionSource.Task;
        }

        public WeekMenuDto GetWeekMenu() {
            var now = DateTime.UtcNow;

            var availableDishes = _repo.All<DishItem>()
                .Include(x => x.AvailableOn)
                .Include(x => x.Supplier)
                .Include(x => x.Category)
                .Where(x => x.AvailableUntil >= now);

            List<(string dayName, DishItem dish)> dayNameDishPairs = new List<(string, DishItem)>();
            foreach (var dish in availableDishes) {
                var dayIds = new HashSet<int>(dish.AvailableOn.Select(x => x.WeekDayId));
                var dayNames = _repo.All<WeekDay>().Where(x => dayIds.Contains(x.Id)).Select(x => x.Name);
                foreach (var dayName in dayNames) {
                    dayNameDishPairs.Add((dayName: dayName, dish: dish));
                }
            }

            var dishesByDayName = dayNameDishPairs.ToLookup(x => x.dayName, x => x.dish);

            var dto = new WeekMenuDto {
                WeekDays = dishesByDayName.Select(x => new WeekDayDto {
                    WeekDay = x.Key,
                    Suppliers = x.GroupBy(d => (id: d.Supplier.Id, name: d.Supplier.Name)).Select(d => {
                        var dishItemByPairPairs =
                            d.Select(di => (key: (categoryId: di.Category.Id, categoryName: di.Category.Name),
                                value: di));
                        var dishItemByPair = dishItemByPairPairs.ToLookup(y => y.key, y => y.value);
                        return new SupplierDto {
                            SupplierId = d.Key.id,
                            SupplierName = d.Key.name,
                            Categories = dishItemByPair.Select(z => new CategoryDto {
                                Id = z.Key.categoryId,
                                Name = z.Key.categoryName,
                                Dishes = z.Select(f => new DishDto {
                                    Id = f.Id,
                                    Name = f.Name,
                                    Price = f.Price,
                                    NegativeRewievs = f.NegativeReviews,
                                    PositiveRewievs = f.PositiveReviews
                                }).ToArray()
                            }).ToArray()
                        };
                    }).OrderBy(t => t.SupplierId).ToArray()
                }).ToArray()
            };

            return dto;
        }

        private async Task SaveChanges(List<SupplierDto> food, TaskCompletionSource<Unit> completionSource) {
            var categories = _repo.All<DishCategory>().ToList();
            categories.AddRange(food.SelectMany(x => x.Categories.Select(c => new DishCategory {Name = c.Name})));
            var dishItems = food.SelectMany(x => x.Categories.SelectMany(c =>
                c.Dishes.Select(d =>
                    new DishItem {
                        Name = d.Name,
                        Price = d.Price,
                        AvailableUntil = DateTime.Today.Next(DayOfWeek.Friday),
                        AvailableOn = d.WeekDay.Select(g => new DishItemToWeekDay {
                            WeekDayId = (int) g
                        }).ToList(),
                        SupplierKey = x.SupplierId,
                        Category = categories.FirstOrDefault(dc => dc.Name == c.Name) ??
                                   new DishCategory {Name = c.Name}
                    }))).ToDictionary(x => new DishKey(x.Name, x.SupplierKey));

            DishItem[] allDishes = _repo.All<DishItem>().Include(x => x.AvailableOn).ToArray();
            DishItem[] existingItems = allDishes.Intersect(dishItems.Values, new DishKeyComparer()).ToArray();

            foreach (DishItem dishItem in existingItems) {
                var item = dishItems[new DishKey(dishItem.Name, dishItem.SupplierKey)];
                dishItem.Price = item.Price;
                dishItem.Category = item.Category;
                dishItem.AvailableUntil = DateTime.Today.Next(DayOfWeek.Friday);
                dishItem.AvailableOn = item.AvailableOn;
                _repo.Update(dishItem);
            }

            await _repo.InsertAsync(dishItems.Values.Except(allDishes, new DishKeyComparer()));
            
            try {
                _repo.Save();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

            completionSource.SetResult(Unit.Default);
        }

        private class DishKeyComparer : IEqualityComparer<DishItem> {
            public bool Equals(DishItem x, DishItem y) {
                if (ReferenceEquals(null, y)) return false;
                if (ReferenceEquals(x, y)) return true;
                return string.Equals(x.Name, y.Name) && x.SupplierKey == y.SupplierKey;
            }

            public int GetHashCode(DishItem obj) {
                unchecked {
                    return ((obj.Name != null ? obj.Name.GetHashCode() : 0) * 397) ^ obj.SupplierKey;
                }
            }
        }

        private class DishKey : IEquatable<DishKey> {
            public DishKey(string name, int supplierId) {
                Name = name;
                SupplierId = supplierId;
            }

            private string Name { get; }
            private int SupplierId { get; }

            public bool Equals(DishKey other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) &&
                       SupplierId == other.SupplierId;
            }

            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((DishKey) obj);
            }

            public override int GetHashCode() {
                unchecked {
                    return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ SupplierId;
                }
            }
        }
    }
}