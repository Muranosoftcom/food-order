using FoodOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.Persistence.Contexts {
    public interface IFoodOrderDbContext {
        DbSet<Dish> DishItems { get; set; }

        DbSet<DishCategory> DishCategories { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }

        DbSet<Order> Orders { get; set; }

        DbSet<User> Users { get; set; }
        DbSet<Day> Calendar { get; set; }
    }
}