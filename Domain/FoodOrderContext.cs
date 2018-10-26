using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public class FoodOrderContext : DbContext
    {
        public FoodOrderContext (DbContextOptions<FoodOrderContext> options)
            : base(options) { }

        public DbSet<DishItem> DishItems { get; set; }
        
//        public DbSet<DishCategory> DishCategories { get; set; }
        
        public DbSet<WeekDay> WeekDays { get; set;         }
        
        public DbSet<DishItemToWeekDay> DishItemsToWeekDays { get; set;}
                
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishItemToWeekDay>().HasKey(x => new {x.DishItemId, x.WeekDayId});
        }
    }
}