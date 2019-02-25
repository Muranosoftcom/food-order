using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class DayConfiguration: IEntityTypeConfiguration<Day> {
		public void Configure(EntityTypeBuilder<Day> builder) {
			builder.ToTable("Calendar");
			builder.HasKey(d => d.Date);
			builder.Property(d => d.Date)
				.HasColumnType("date")
				.IsRequired();
			
			builder.Property(x => x.IsHoliday)
				.HasDefaultValue(false);
		}
	}
}