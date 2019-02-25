using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class DishCategoryConfiguration: IEntityTypeConfiguration<DishCategory> {
		public void Configure(EntityTypeBuilder<DishCategory> builder) {
			builder.Property(c => c.Name)
				.HasMaxLength(100)
				.IsRequired();
			
			builder
				.HasMany(c => c.DishItems)
				.WithOne(d => d.Category)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}