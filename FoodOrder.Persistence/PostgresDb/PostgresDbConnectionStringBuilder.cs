namespace FoodOrder.Persistence.PostgresDb {
	public class PostgresDbConnectionStringBuilder : IConnectionStringBuilder {
		private readonly IDatabaseConfig _config;

		public PostgresDbConnectionStringBuilder(IDatabaseConfig config) {
			_config = config;
		}
		
		public string GetConnectionString() {
			return $"host={_config.ServerName};" +
				   $"database={_config.DatabaseName};" +
				   $"username={_config.User};" +
				   $"password={_config.Password};";
		}
	}
}