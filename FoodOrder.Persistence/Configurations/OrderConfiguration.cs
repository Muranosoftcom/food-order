using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class OrderConfiguration: IEntityTypeConfiguration<Order> {
		public void Configure(EntityTypeBuilder<Order> builder) {
			builder
				.Property(o => o.Price)
				.HasColumnType("Money");
			
			builder
				.HasOne(o => o.User)
				.WithMany(user => user.Orders);

			builder
				.HasMany(o => o.OrderItems)
				.WithOne(oi => oi.Order);
		}
	}
}