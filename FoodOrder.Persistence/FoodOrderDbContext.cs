using FoodOrder.Domain.Entities;
using FoodOrder.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodOrder.Persistence {
    public class FoodOrderDbContext : DbContext, IDesignTimeDbContextFactory<FoodOrderDbContext>, IFoodOrderContext {
        public FoodOrderDbContext() { }
        public FoodOrderDbContext(DbContextOptions options): base(options) { }

        public FoodOrderDbContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderDbContext>();
            var dbContextConnectionBuilder = new DesignTimeDbContextConnectionBuilder();

            return new FoodOrderDbContext(dbContextConnectionBuilder.ConfigureDbContextOptionsBuilder(optionsBuilder).Options);
        }

        public static DbContextOptionsBuilder ConfigureDbContextOptionsBuilder(DbContextOptionsBuilder optionsBuilder, 
            IDbContextConnectionBuilder dbContextConnectionBuilder) {
            return dbContextConnectionBuilder.ConfigureDbContextOptionsBuilder(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<DishCategory> DishCategories { get; set; }
        public DbSet<DishItem> DishItems { get; set; }
        public DbSet<DishItemToWeekDay> DishItemsToWeekDays { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyAllConfigurations();
            modelBuilder.Seed();
        }
    }
}