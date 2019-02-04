using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class DishItemToWeekDayConfiguration: IEntityTypeConfiguration<DishItemToWeekDay> {
		public void Configure(EntityTypeBuilder<DishItemToWeekDay> builder) {
			builder.HasKey(x => new {x.DishItemId, x.WeekDayId});
			builder.HasOne(bc => bc.WeekDay)
				.WithMany(b => b.AvailableItems)
				.HasForeignKey(bc => bc.WeekDayId);
			
			builder.HasOne(bc => bc.DishItem)
				.WithMany(c => c.AvailableOn)
				.HasForeignKey(bc => bc.DishItemId);
		}
	}
}