namespace FoodOrder.Persistence {
	public interface IDatabaseConfig {
		string ServerName { get; }
		string DatabaseName { get; }
		string Password { get; }
		string User { get; }
	}
}