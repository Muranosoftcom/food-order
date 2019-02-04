using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence.DbContexts {
    public interface IFoodOrderContext {
        DbSet<DishItem> DishItems { get; set; }

        DbSet<DishCategory> DishCategories { get; set; }

        DbSet<WeekDay> WeekDays { get; set; }

        DbSet<DishItemToWeekDay> DishItemsToWeekDays { get; set; }

        DbSet<OrderItem> OrderItems { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<User> Users { get; set; }
    }
}