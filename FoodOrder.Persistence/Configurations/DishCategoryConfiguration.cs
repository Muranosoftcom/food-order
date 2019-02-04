using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class DishCategoryConfiguration: IEntityTypeConfiguration<DishCategory> {
		public void Configure(EntityTypeBuilder<DishCategory> builder) {
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}