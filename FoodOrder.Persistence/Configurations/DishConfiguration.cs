using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class DishConfiguration: IEntityTypeConfiguration<Dish> {
		public void Configure(EntityTypeBuilder<Dish> builder) {
			builder
				.Property(d => d.Name)
				.HasMaxLength(100)
				.IsRequired();

			builder
				.Property(d => d.Price)
				.HasColumnType("Money");
			
			builder
				.Property(d => d.InternalDayOfWeekArray)
				.HasMaxLength(15)
				.HasColumnName("DayOfWeeks");

			builder.Ignore(d => d.AvailableAt);
			builder.Ignore(d => d.IsAvailable);
		}
	}
}