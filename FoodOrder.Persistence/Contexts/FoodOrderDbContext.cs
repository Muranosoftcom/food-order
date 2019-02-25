using FoodOrder.Domain.Entities;
using FoodOrder.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodOrder.Persistence.Contexts {
    public class FoodOrderDbContext : DbContext, IDesignTimeDbContextFactory<FoodOrderDbContext>, IFoodOrderDbContext {
        public FoodOrderDbContext() { }
        public FoodOrderDbContext(DbContextOptions options): base(options) { }

        public FoodOrderDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderDbContext>();
            var dbContextConnectionBuilder = new DesignTimeDbContextConnectionBuilder();

            return new FoodOrderDbContext(dbContextConnectionBuilder.ConfigureDbContextOptionsBuilder(optionsBuilder).Options);
        }

        public static void ConfigureDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder, 
            IDbContextConnectionBuilder dbContextConnectionBuilder) {
            dbContextConnectionBuilder.ConfigureDbContextOptionsBuilder(optionsBuilder);
        }

        public DbSet<Day> Calendar { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<Dish> DishItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyAllConfigurations();
            modelBuilder.Seed();
        }
    }
}