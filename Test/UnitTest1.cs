using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain;
using Domain.Contexts;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using SpreadsheetIntegration.Core;
using SpreadsheetIntegration.Google;
using Xunit;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
//            var kafeResult = new GoogleSpreadsheetProvider(GoogleClientServiceProvider.GetService()).Get(
//                "1sbbFJqa-X4KW91EmPyO9NTbP8bbuwg3szCSC3FTeR2c", new SpreadsheetGetRequest("Menu", "A1:E100"));
//
//            List<object> objects = new List<object>();
//            string lastCategory = string.Empty;
//
//            Regex rx = new Regex("^(.*?)(?<price>\\d{1,3})$");
//
////            foreach (var row in kafeResult.AsEnumerable())
////            {
////
////                foreach (var cell in row.Where(x => !string.IsNullOrEmpty(x.Value))
////                    .Select((x, index) => new {x, index = index + 1}))
////                {
////                    if (cell.x.Value.StartsWith("__"))
////                    {
////                        lastCategory = cell.x.Value.Trim('_');
////                    }
////                    else
////                    {
////                        objects.Add(new
////                        {
////                            Category = lastCategory,
////                            Name = cell.x.Value,
////                            Price = rx.Match(cell.x.Value).Groups["price"].Value,
////                            Day = (DayOfWeek) cell.index
////                        });
////                    }
////                }
////            }
//
//            var glagolResult = new GoogleSpreadsheetProvider(GoogleClientServiceProvider.GetService()).Get(
//                "1ik4DnYQL3hCbxF4PRmmeaGhcTXYW4BcCbGvtijoK_rk", new SpreadsheetGetRequest("Menu", "A2:F30"));
//
//            
//        }
        }

        [Fact]
        public void TestDb()
        {
            var context = new FoodOrderContext().CreateDbContext(null);
            context.WeekDays.Add(new WeekDay {Name = "Wd"});
            context.SaveChanges();
            var t = context.WeekDays.FirstOrDefault(x => x.Name == "Wd");            
            Assert.Equal(t.Name, "Wd");
            
            context.Remove(t);
            context.SaveChanges();
            var w = context.WeekDays.FirstOrDefault(x => x.Name == "Wd");
            Assert.Null(w);            
        }

        [Fact]
        public async Task AddDishItem()
        {
            var context = new FoodOrderContext().CreateDbContext(null);
            var repo = new FoodOrderRepository(context);
            var date = DateTime.Now;
            
            var dishItem = new DishItem
            {
                Name = "Salo",
                Price = 100,
                Category = new DishCategory
                {
                    Name = "Еда богов"
                },
                Supplier = repo.All<Supplier>().FirstOrDefault(x => x.Name == "ГлаголЪ"),
                AvailableOn = new List<DishItemToWeekDay> {new DishItemToWeekDay {WeekDay = repo.GetById<WeekDay>(1)}},
                AvailableUntil = date
            };

            await repo.InsertAsync(dishItem);
            await repo.SaveAsync();
            var i = repo.All<DishItem>().Include(x => x.AvailableOn).FirstOrDefault(x => x.AvailableUntil == date);
            Assert.Equal(dishItem.AvailableOn.First().DishItemId, i.AvailableOn.First().DishItemId);
            Assert.Equal(dishItem.AvailableOn.First().WeekDayId, i.AvailableOn.First().WeekDayId);
        }
    }
}