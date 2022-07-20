using Backend.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Repository
{
    public interface IUserRepository
    {
        //Users
        public Task<IEnumerable<UserEntity>> GetUsersAsync();
        public Task<UserEntity> GetUserAsync(int userId);
        public void CreateUser(UserEntity user);
        public Task UpdateUserAsync(int userId, UserEntity user);
        public Task DeleteUserAsync(int userId);

        Task<bool> SaveChangesAsync();
    }
}
