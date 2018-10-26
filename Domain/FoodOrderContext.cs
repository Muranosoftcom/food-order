using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Domain
{
    public class FoodOrderContext : DbContext, IDesignTimeDbContextFactory<FoodOrderContext>
    {
        public FoodOrderContext(DbContextOptions<FoodOrderContext> options)
            : base(options)
        {
        }

        public FoodOrderContext()
        {
            
        }

        public DbSet<DishItem> DishItems { get; set; }

//        public DbSet<DishCategory> DishCategories { get; set; }

        public DbSet<WeekDay> WeekDays { get; set; }

        public DbSet<DishItemToWeekDay> DishItemsToWeekDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

        public FoodOrderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderContext>();
            optionsBuilder.UseSqlServer("Server=ua-muk100;Database=foodOrderDatabase;User id=login;Password=12345;");

            return new FoodOrderContext(optionsBuilder.Options);
        }
    }
}