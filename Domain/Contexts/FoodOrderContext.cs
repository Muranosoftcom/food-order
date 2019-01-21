using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Domain.Contexts {
    public class FoodOrderContext : DbContext, IDesignTimeDbContextFactory<FoodOrderContext>, IFoodOrderContext {
        public FoodOrderContext(DbContextOptions<FoodOrderContext> options)
            : base(options) { }

        public FoodOrderContext() { }

        public FoodOrderContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());

            return new FoodOrderContext(optionsBuilder.Options);
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
            modelBuilder.Entity<DishItemToWeekDay>().HasKey(x => new {x.DishItemId, x.WeekDayId});
            modelBuilder.Entity<DishItemToWeekDay>()
                .HasOne(bc => bc.WeekDay)
                .WithMany(b => b.AvailableItems)
                .HasForeignKey(bc => bc.WeekDayId);

            modelBuilder.Entity<DishItemToWeekDay>()
                .HasOne(bc => bc.DishItem)
                .WithMany(c => c.AvailableOn)
                .HasForeignKey(bc => bc.DishItemId);
        }
    }
}