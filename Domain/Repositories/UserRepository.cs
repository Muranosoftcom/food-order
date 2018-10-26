using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contexts;
using Domain.Entities;

namespace Domain.Repositories
{
    public class UserRepository : EditableRepositoryBase<User>
    {
        public UserRepository(IFoodOrderContext context) : base(context)
        {
        }

        public override IQueryable<User> All()
        {
            return _context.Users.AsQueryable();
        }

        public override User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public override async Task Insert(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public override Task Update(User entity)
        {
          throw new NotImplementedException();
        }

        public override Task Delete(User entity)
        {
            throw new NotImplementedException();

        }
    }
}