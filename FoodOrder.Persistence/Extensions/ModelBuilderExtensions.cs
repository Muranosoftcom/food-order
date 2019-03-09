using System;
using System.Linq;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Enumerations;
using FoodOrder.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence.Extensions {
	public static class ModelBuilderExtensions {
		public static void ApplyAllConfigurations(this ModelBuilder modelBuilder) {
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new DayConfiguration());
			
			modelBuilder.ApplyConfiguration(new SupplierConfiguration());
			modelBuilder.ApplyConfiguration(new DishCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new DishConfiguration());

			/*modelBuilder.ApplyConfiguration(new OrderConfiguration());
			modelBuilder.ApplyConfiguration(new OrderItemConfiguration());*/
		}
		
		public static void Seed(this ModelBuilder modelBuilder) {
			modelBuilder.Entity<Supplier>().HasData(
				new Supplier {
					Id = SupplierType.Cafe.Id,
					Position = 0,
					Name = "Столовая №1",
					CanMultiSelect = true,
					AvailableMoneyToOrder = 68
				},
				new Supplier {
					Id = SupplierType.Glagol.Id,
					Position = 1,
					Name = "ГлаголЪ",
					CanMultiSelect = false,
					AvailableMoneyToOrder = 0,
				}
			);
			
			modelBuilder.Entity<DishCategory>().HasData(
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Салаты",
					Position = 0,
					SupplierId = SupplierType.Cafe.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Первое",
					Position = 1,
					SupplierId = SupplierType.Cafe.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Мясное",
					Position = 2,
					SupplierId = SupplierType.Cafe.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Position = 3,
					Name = "Гарниры",
					SupplierId = SupplierType.Cafe.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Десерты",
					Position = 4,
					SupplierId = SupplierType.Cafe.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Пироги",
					Position = 5,
					SupplierId = SupplierType.Cafe.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Напитки",
					Position = 6,
					SupplierId = SupplierType.Cafe.Id
				},
				
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Первые блюда",
					Position = 0,
					SupplierId = SupplierType.Glagol.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Name = "Вторые блюда",
					Position = 1,
					SupplierId = SupplierType.Glagol.Id
				},
				new DishCategory {
					Id = Guid.NewGuid(),
					Position = 2,
					Name = "Салаты, пирожки",
					SupplierId = SupplierType.Glagol.Id
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