using Backend.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private UserDBContext _dbContext;

        public UserRepository(UserDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public void CreateUser(UserEntity user)
        {
            _dbContext.Users.Add(user);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var deleteToUser = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            _dbContext.Users.Remove(deleteToUser);
        }

        public async Task<UserEntity> GetUserAsync(int userId)
        {
            IQueryable<UserEntity> query = _dbContext.Users;
            query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync()
        {
            IQueryable<UserEntity> query = _dbContext.Users;
            query = query.AsNoTracking();
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var result = await _dbContext.SaveChangesAsync();
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateUserAsync(int userId, UserEntity user)
        {
            var userToUpdate = await _dbContext.Users.FirstAsync(u => u.Id == userId);

            userToUpdate.FirstName = user.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = user.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = user.Email ?? userToUpdate.Email;
            userToUpdate.Password = user.Password ?? userToUpdate.Password;
        }
    }
}
