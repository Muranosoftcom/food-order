using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence {
	public interface IDbContextConnectionBuilder {
		DbContextOptionsBuilder ConfigureDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder);
	}
}