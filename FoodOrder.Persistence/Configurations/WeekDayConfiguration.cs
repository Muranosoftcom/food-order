using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class WeekDayConfiguration: IEntityTypeConfiguration<WeekDay> {
		public void Configure(EntityTypeBuilder<WeekDay> builder) {
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(15);
		}
	}
}