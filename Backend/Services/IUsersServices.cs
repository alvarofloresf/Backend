using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public interface IUsersServices
    {
        public Task<IEnumerable<UserModel>> GetAllUsersAsync();
        public Task<UserModel> GetUserAsync(int userId);
        public Task<UserModel> CreateUserAsync(UserModel user);
        public Task<UserModel> UpdateUserAsync(int userId, UserModel user);
        public Task<bool> DeleteUserAsync(int userId);
    }
}
