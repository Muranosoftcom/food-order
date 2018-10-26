using System.Linq;
using System.Threading.Tasks;
using Domain.Contexts;
using Domain.Entities;

namespace Domain.Repositories
{
    public class DishRepository : EditableRepositoryBase<DishItem>
    {
        public DishRepository(IFoodOrderContext context) : base(context)
        {
        }

        public override IQueryable<DishItem> All()
        {
            throw new System.NotImplementedException();
        }

        public override DishItem GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public override Task Insert(DishItem entity)
        {
            throw new System.NotImplementedException();
        }

        public override Task Update(DishItem entity)
        {
            throw new System.NotImplementedException();
        }

        public override Task Delete(DishItem entity)
        {
            throw new System.NotImplementedException();
        }
    }
}