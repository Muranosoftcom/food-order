using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Domain.Contexts
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

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier {Id = 1, Name = "Столовая №1"},
                new Supplier {Id = 2, Name = "ГлаголЪ"});

            modelBuilder.Entity<WeekDay>().HasData(
                new WeekDay {Id = 1, Name = "Mon"},
                new WeekDay {Id = 2, Name = "Tue"},
                new WeekDay {Id = 3, Name = "Wed"},
                new WeekDay {Id = 4, Name = "Thu"},
                new WeekDay {Id = 5, Name = "Fri"},
                new WeekDay {Id = 6, Name = "Sat"},
                new WeekDay {Id = 7, Name = "Sun"}
            );
        }

        public FoodOrderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderContext>();
            optionsBuilder.UseSqlServer("Server=ua-muk100;Database=foodOrderDatabase;User id=login;Password=12345;");

            return new FoodOrderContext(optionsBuilder.Options);
        }
    }
}