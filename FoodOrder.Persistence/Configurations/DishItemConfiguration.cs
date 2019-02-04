using System;
using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class DishItemConfiguration: IEntityTypeConfiguration<DishItem> {
		public void Configure(EntityTypeBuilder<DishItem> builder) {
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(x => x.Price)
				.HasColumnType("Money");
			
			throw new NotImplementedException();
		}
	}
}