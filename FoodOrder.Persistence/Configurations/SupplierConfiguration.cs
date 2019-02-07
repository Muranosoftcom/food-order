using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class SupplierConfiguration: IEntityTypeConfiguration<Supplier> {
		public void Configure(EntityTypeBuilder<Supplier> builder) {
			builder.Property(s => s.Name)
				.HasMaxLength(20)
				.IsRequired();

			builder.Property(s => s.AvailableMoneyToOrder)
				.HasColumnType("Money");

			builder.Property(s => s.Position)
				.HasDefaultValue(1);

			builder
				.HasMany(s => s.Categories)
				.WithOne(c => c.Supplier);
			
			builder
				.HasMany(s => s.DishItems)
				.WithOne(d => d.Supplier);
		}
	}
}