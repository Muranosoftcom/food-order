using System.Linq;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Enumerations;
using FoodOrder.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence.Extensions {
	public static class ModelBuilderExtensions {
		public static void ApplyAllConfigurations(this ModelBuilder modelBuilder) {
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			
			modelBuilder.ApplyConfiguration(new SupplierConfiguration());
			modelBuilder.ApplyConfiguration(new DishCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new DishItemConfiguration());
			modelBuilder.ApplyConfiguration(new DishItemToWeekDayConfiguration());
			modelBuilder.ApplyConfiguration(new WeekDayConfiguration());
			
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
			modelBuilder.ApplyConfiguration(new DayConfiguration());
		}
		
		public static void Seed(this ModelBuilder modelBuilder) {
			modelBuilder.Entity<Supplier>().HasData(
				new Supplier {
					Id = (int) FoodSupplier.Cafe,
					Position = 0,
					Name = "Столовая №1",
					CanMultiSelect = true,
					AvailableMoneyToOrder = 68,
				},
				
				new Supplier {
					Id = (int) FoodSupplier.Glagol,
					Position = 1,
					Name = "ГлаголЪ",
					CanMultiSelect = false,
					AvailableMoneyToOrder = 0,
				}
			);

			modelBuilder.Entity<WeekDay>().HasData(
				new WeekDay {
					Id = (int) DayOfWeek.Monday,
					Name = "Mon"
				},

				new WeekDay {
					Id = (int) DayOfWeek.Tuesday,
					Name = "Tue"
				},

				new WeekDay {
					Id = (int) DayOfWeek.Wednesday,
					Name = "Wed"
				},

				new WeekDay {
					Id = (int) DayOfWeek.Thursday,
					Name = "Thu"
				},

				new WeekDay {
					Id = (int) DayOfWeek.Friday,
					Name = "Fri"
				},

				new WeekDay {
					Id = (int) DayOfWeek.Saturday,
					Name = "Sat"
				},
				
				new WeekDay {
					Id = (int) DayOfWeek.Sunday,
					Name = "Sun"
				}
			);
			
			string[] holidays = {
				"2019-01-01",
				"2019-01-07",
				"2019-03-08",
				"2019-04-28",
				"2019-04-29",
				"2019-05-01",
				"2019-05-08",
				"2019-05-09",
				"2019-06-17",
				"2019-06-28",
				"2019-08-24",
				"2019-08-26",
				"2019-10-14",
				"2019-11-21",
				"2019-12-25",
			};
			
			modelBuilder.Entity<Day>().HasData(
				holidays.Select(day => Day.Create(day, true))
			);
		}
	}
	
}