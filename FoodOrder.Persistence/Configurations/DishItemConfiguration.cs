using System;
using System.Data;
using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class DishItemConfiguration: IEntityTypeConfiguration<DishItem> {
		public void Configure(EntityTypeBuilder<DishItem> builder) {
			builder.Property(x => x.Name)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(x => x.Price)
				.HasColumnType("Money");
		}
	}
	
	
}