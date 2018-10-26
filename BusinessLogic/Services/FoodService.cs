using System;
using System.Collections.Generic;
using System.Linq;
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
using SpreadsheetIntegration;
using SpreadsheetIntegration.Core;

namespace BusinessLogic.Services
{
    public class FoodService
    {
        private readonly IRepository _repo;
        private readonly IAsyncSpreadsheetProvider _spreadsheetProvider;

        public FoodService(IRepository repo, IAsyncSpreadsheetProvider spreadsheetProvider)
        {
            _repo = repo;
            _spreadsheetProvider = spreadsheetProvider;
        }

        public async Task SynchronizeFood()
        {
            IObservable<ValuesRange> glagolFood = _spreadsheetProvider.GetAsync(
                "1ik4DnYQL3hCbxF4PRmmeaGhcTXYW4BcCbGvtijoK_rk",
                new SpreadsheetGetRequest("Menu", "A2:F30"), CancellationToken.None).ToObservable();

            IObservable<ValuesRange> kafeFood = _spreadsheetProvider.GetAsync(
                "1sbbFJqa-X4KW91EmPyO9NTbP8bbuwg3szCSC3FTeR2c",
                new SpreadsheetGetRequest("Menu", "A1:E100"), CancellationToken.None).ToObservable();

            var glagolFoodDto = glagolFood.Select(x => ParsingRegistry.GetParser(FoodProvider.Glagol).ExtractFood(x))
                .Select(x =>
                {
                    var food = new FoodDTO
                    {
                        SupplierId = (int) FoodProvider.Glagol,
                        SupplierName = "ГлаголЪ",
                        Categories = x.GroupBy(g => g.Category).Select(c => new CategoryDTO
                        {
                            Name = c.Key,
                            Dishes = c.GroupBy(g => (g.Name, g.Price)).Select(d => new DishDTO
                            {
                                Name = d.Key.Item1,
                                Price = d.Key.Item2,
                                WeekDay = d.Select(g => g.Day).ToArray()
                            }).ToArray()
                        }).ToArray()
                    };

                    return food;
                });

            var kafeFoodDto = kafeFood.Select(x => ParsingRegistry.GetParser(FoodProvider.Kafe).ExtractFood(x)).Select(
                x =>
                {
                    var food = new FoodDTO
                    {
                        SupplierId = (int) FoodProvider.Kafe,
                        SupplierName = "Столовая",
                        Categories = x.GroupBy(g => g.Category).Select(c => new CategoryDTO
                        {
                            Name = c.Key,
                            Dishes = c.GroupBy(g => (g.Name, g.Price)).Select(d => new DishDTO
                            {
                                Name = d.Key.Item1,
                                Price = d.Key.Item2,
                                WeekDay = d.Select(g => g.Day).ToArray()
                            }).ToArray()
                        }).ToArray()
                    };

                    return food;
                });

            glagolFoodDto.Merge(kafeFoodDto).Aggregate(new List<FoodDTO>(), (x, y) =>
            {
                x.Add(y);
                return x;
            }).Subscribe(SaveChanges);
        }

        private void SaveChanges(List<FoodDTO> food)
        {
            Dictionary<DishKey, DishItem> dishItems = food.SelectMany(x => x.Categories.SelectMany(c => c.Dishes.Select(d => new DishItem
            {
                Name = d.Name,
                Price = d.Price,
                AvailableUntil = DateTime.Today,
                AvailableOn = d.WeekDay.Select(g => new DishItemToWeekDay
                {
                    WeekDayId = (int) g
                }).ToList(),
                SupplierKey = x.SupplierId,
                Category = new DishCategory
                {
                    Name = c.Name
                }
            }))).ToDictionary(x => new DishKey(x.Name, x.SupplierKey));

            DishItem[] existingItems = _dishRepository.All().Where(x => dishItems.ContainsKey(new DishKey(x.Name, x.SupplierKey))).ToArray();

            foreach (var dishItem in existingItems.Except(dishItems.Values, new DishPriceComparer()))
            {
                dishItem.Price = dishItems[new DishKey(dishItem.Name, dishItem.SupplierKey)].Price;
                _dishRepository.Update(dishItem);
            }
        }

        private class DishPriceComparer : IEqualityComparer<DishItem>
        {
            public bool Equals(DishItem x, DishItem y)
            {
                return x.Price == y.Price;
            }

            public int GetHashCode(DishItem obj)
            {
                return obj.Price.GetHashCode();
            }
        }

        private class DishKey : IEquatable<DishKey>
        {
            public DishKey(string name, int supplierId)
            {
                Name = name;
                SupplierId = supplierId;
            }
            
            public string Name { get; }
            public int SupplierId { get; }

            public bool Equals(DishKey other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase) && SupplierId == other.SupplierId;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((DishKey) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ SupplierId;
                }
            }
        }
    }
}