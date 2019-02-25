using FoodOrder.Persistence.PostgresDb;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence {
	internal class DesignTimeDbContextConnectionBuilder: IDbContextConnectionBuilder {
		public DbContextOptionsBuilder ConfigureDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder) {
			var dbConfig = new DesignTimeDbConfig();
			return optionsBuilder.UseNpgsql(new PostgresDbConnectionStringBuilder(dbConfig).GetConnectionString());
		}
	}
	
	internal class DesignTimeDbConfig: IDatabaseConfig {
		public DesignTimeDbConfig() {
			ServerName = "localhost";
			DatabaseName = "testDb";
			User = "fakeUser";
			Password = "fakePass";
		}
		
		public string ServerName { get; }
		public string DatabaseName { get; }
		public string Password { get; }
		public string User { get; }
	}
}