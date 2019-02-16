using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence {
	public class PostgresDbContextConnectionBuilder: IDbContextConnectionBuilder {
		private readonly IConnectionStringBuilder _connectionStringBuilder;

		public PostgresDbContextConnectionBuilder(IConnectionStringBuilder connectionStringBuilder) {
			_connectionStringBuilder = connectionStringBuilder;
		}
		
		public DbContextOptionsBuilder ConfigureDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) {
			return optionsBuilder.UseNpgsql(_connectionStringBuilder.GetConnectionString());
		}
	}
}