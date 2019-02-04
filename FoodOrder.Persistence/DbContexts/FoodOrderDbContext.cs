using FoodOrder.Domain.Entities;
using FoodOrder.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodOrder.Persistence.DbContexts {
    public class FoodOrderDbContext : DbContext, IDesignTimeDbContextFactory<FoodOrderDbContext>, IFoodOrderContext {
        public FoodOrderDbContext(DbContextOptions<FoodOrderDbContext> options)
            : base(options) { }

        public FoodOrderDbContext() { }

        public FoodOrderDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderDbContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());

            return new FoodOrderDbContext(optionsBuilder.Options);
        }

        public static string GetConnectionString(string databaseName = "FoodOrderDatabase", string databaseUser = "login", string databasePass = "12345", string server = "localhost") {
               return $"Server={server};" +
                   $"Database={databaseName};" +
                   $"User id={databaseUser};" +
                   $"Password={databasePass};";
        }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<DishItem> DishItems { get; set; }

        public DbSet<DishCategory> DishCategories { get; set; }

        public DbSet<WeekDay> WeekDays { get; set; }

        public DbSet<DishItemToWeekDay> DishItemsToWeekDays { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyAllConfigurations();
            
            
        }
    }
}