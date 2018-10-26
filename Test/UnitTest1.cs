using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Domain;
using Domain.Entities;
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
//            foreach (var row in glagolResult.AsEnumerable())
//            {
//                lastCategory = row.First().Value;
//                foreach (var cell in row.Skip(1).Where(x => !string.IsNullOrEmpty(x.Value))
//                    .Select((x, index) => new {x, index = index + 1}))
//                {
//                    objects.Add(new
//                    {
//                        Category = lastCategory,
//                        Name = cell.x.Value,
//                        Price = 0,
//                        Day = (DayOfWeek) cell.index
//                    });
//                }
//            }
//        }
        }

        [Fact]
        public void TestDb()
        {
            var context = new FoodOrderContext().CreateDbContext(null);
            context.WeekDays.Add(new WeekDay {Name = "Wd"});
            context.SaveChanges();
            var t = context.WeekDays.First();
            Assert.Equal(t.Name, "Wd");
        }
    }
}