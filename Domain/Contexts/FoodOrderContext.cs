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
                new Supplier {Name = "Столовая №1"},
                new Supplier {Name = "ГлаголЪ"});

            modelBuilder.Entity<WeekDay>().HasData(
                new WeekDay {Name = "Mon"},
                new WeekDay{Name = "Tue"},
                new WeekDay{Name = "Wed"},
                new WeekDay{Name = "Thu"},
                new WeekDay{Name = "Fri"},
                new WeekDay{Name = "Sat"},
                new WeekDay{Name = "Sun"}
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