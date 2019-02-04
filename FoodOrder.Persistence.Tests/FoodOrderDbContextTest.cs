using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodOrder.BusinessLogic.Services;
using FoodOrder.Domain.Entities;
using FoodOrder.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using FoodOrder.SpreadsheetIntegration.Google;
using Xunit;

namespace FoodOrder.Persistence.Tests {
    public class FoodOrderDbContextTest {
        [Fact]
        public async Task AddDishItem() {
            var context = new FoodOrderDbContext().CreateDbContext(null);
            var repo = new FoodOrderRepository(context);
            var date = DateTime.Now;

            var dishItem = new DishItem {
                Name = "Salo",
                Price = 100,
                Category = new DishCategory {
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

        [Fact]
        public void FillDatabaseWithUsers() {
            var context = new FoodOrderDbContext().CreateDbContext(null);
            var repo = new FoodOrderRepository(context);

            var prevCount = repo.All<User>().Count();

            repo.InsertAsync(
                new[] {
                    new User {
                        Email = "fake_email@mail.com",
                        UserName = "FakeFirst FakeLast"
                    },
                    new User {
                        Email = "admin@mail.com",
                        UserName = "Admin Admin"
                    },
                    new User {
                        Email = "egor@mail.com",
                        UserName = "Egor Manevich"
                    },
                    new User {
                        Email = "human@mail.com",
                        UserName = "Human Human"
                    }
                });
            repo.Save();
            var currentCount = repo.All<User>().Count();
            var diff = currentCount - prevCount;
            Assert.Equal(4, diff);
        }

        [Fact]
        public void TestDb() {
            var context = new FoodOrderDbContext().CreateDbContext(null);
            context.WeekDays.Add(new WeekDay {Name = "Wd"});
            context.SaveChanges();
            var t = context.WeekDays.FirstOrDefault(x => x.Name == "Wd");
            Assert.Equal("Wd", t.Name);

            context.Remove(t);
            context.SaveChanges();
            var w = context.WeekDays.FirstOrDefault(x => x.Name == "Wd");
            Assert.Null(w);
        }

        [Fact]
        public async Task TestSync() {
            var context = new FoodOrderDbContext().CreateDbContext(null);
            var repo = new FoodOrderRepository(context);
            var service = new FoodService(repo, new GoogleSpreadsheetProvider());
            await service.SynchronizeFood();
        }
    }
}