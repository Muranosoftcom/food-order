using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FoodOrder.Persistence.Configurations {
	public class UserConfiguration: IEntityTypeConfiguration<User> {
		public void Configure(EntityTypeBuilder<User> builder) {
			builder.Property(u => u.UserName)
				.HasMaxLength(256)
				.IsRequired();
			
			builder.Property(u => u.IsDisabled)
				.HasDefaultValue(false);
		}
	}
}