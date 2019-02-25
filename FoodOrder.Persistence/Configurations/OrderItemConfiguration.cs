using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItem> {
		public void Configure(EntityTypeBuilder<OrderItem> builder) {
			builder
				.Property(o => o.Price)
				.HasColumnType("Money");
		}
	}
}