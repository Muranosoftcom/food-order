using Domain.Contexts;
using Domain.Entities;

namespace Domain.Repositories
{
    public class OrderRepository : ReadOnlyRepositoryBase<Order>
    {
        public OrderRepository(IFoodOrderContext context) : base(context)
        {
        }
    }
}