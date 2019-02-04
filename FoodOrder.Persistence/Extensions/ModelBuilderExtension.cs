using FoodOrder.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence.Extensions {
	public static class ModelBuilderExtension {
		public static void ApplyAllConfigurations(this ModelBuilder modelBuilder) {
			modelBuilder.ApplyConfiguration(new DishCategoryConfiguration());
			modelBuilder.ApplyConfiguration(new DishItemConfiguration());
			modelBuilder.ApplyConfiguration(new DishItemToWeekDayConfiguration());
			modelBuilder.ApplyConfiguration(new WeekDayConfiguration());
		}
	}
}