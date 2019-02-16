using FoodOrder.Persistence;
using Microsoft.Extensions.Configuration;

namespace FoodOrder.WebUI.App {
	public class DatabaseConfig: IDatabaseConfig {
		public DatabaseConfig(IConfigurationSection DbSettingsConfigurationSection) {
			DatabaseName = DbSettingsConfigurationSection["DatabaseName"];
			User = DbSettingsConfigurationSection["User"];
			Password = DbSettingsConfigurationSection["Password"];
			ServerName = DbSettingsConfigurationSection["ServerName"];
		}

		public string ServerName { get; set; }
		public string DatabaseName { get; set; }
		public string Password { get; set; }
		public string User { get; set; }
	}
}