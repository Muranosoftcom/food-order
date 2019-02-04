using System;
using System.Threading.Tasks;
using FoodOrder.Domain.Entities;
using FoodOrder.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodOrder.WebUI.Infrastructure {
    public class UserManager {
        private readonly IRepository _userRepository;

        public UserManager(IRepository userRepository) {
            _userRepository = userRepository;
        }
        
        public async Task<User> RegisterNewAsync(User user) {
            await _userRepository.InsertAsync(user);
            await _userRepository.SaveAsync();
            
            return user;
        }

        public async Task<bool> IsRegisteredAsync(string userEmail) {
            var dbUser = await FindUserByEmail(userEmail);
            
            return dbUser != null;
        }

        public Task<User> FindUserByEmail(string email) {
            return _userRepository.All<User>().FirstOrDefaultAsync(user => string.Compare(user.Email, email, StringComparison.CurrentCultureIgnoreCase) == 0);
        }
    }
}