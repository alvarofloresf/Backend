using AutoMapper;
using Backend.Data.Entities;
using Backend.Data.Repository;
using Backend.Exceptions;
using Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class UserServices : IUsersServices
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserServices(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private HashSet<string> _allowedSortValues = new HashSet<string> { };//i don't know research funtionality

        public async Task<UserModel> CreateUserAsync(UserModel user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            _userRepository.CreateUser(userEntity);
            var result = await _userRepository.SaveChangesAsync();
            if(result)
            {
                return _mapper.Map<UserModel>(userEntity);
            }
            throw new Exception("Database Error");
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            await GetUserAsync(userId);
            await _userRepository.DeleteUserAsync(userId);
            var result = await _userRepository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            return true;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var usersEntityList = await _userRepository.GetUsersAsync();
            return _mapper.Map<IEnumerable<UserModel>>(usersEntityList);
        }

        public async Task<UserModel> GetUserAsync(int userId)
        {
            var user = await _userRepository.GetUserAsync(userId);

            if (user == null)
                throw new NotFoundElementException($"the user with id:{userId} does not exists.");

            return _mapper.Map<UserModel>(user);
        }

        public async Task<UserModel> UpdateUserAsync(int userId, UserModel user)
        {
            await GetUserAsync(userId);
            var userEntity = _mapper.Map<UserEntity>(user);
            await _userRepository.UpdateUserAsync(userId, userEntity);
            var result = await _userRepository.SaveChangesAsync();
            if (result)
            {
                return _mapper.Map<UserModel>(userEntity);
            }

            throw new Exception("Database Error.");
        }
    }
}
