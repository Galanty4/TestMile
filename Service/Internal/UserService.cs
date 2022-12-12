using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interfaces.Repositories;
using BLL.Interfaces.Services;
using BLL.Models;

namespace Service.Internal
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> AddAsync(UserDto userDto)
        {
            var user = new User(userDto);
            var entity = await _userRepository.AddAsync(user);
            return new UserDto(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var userEntries = await _userRepository.GetAllAsync();
            return userEntries.Select(user => new UserDto(user));
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            return new UserDto(user);
        }

        public async Task RemoveAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveAsync();
        }

        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            var foundUserEntry = await _userRepository.GetAsync(userDto.Id);

            if (foundUserEntry == null)
            {
                throw new Exception($"invalid {nameof(UserDto)} provided");
            }

            foundUserEntry.Update(userDto);
            await _userRepository.SaveAsync();

            return new UserDto(foundUserEntry);
        }
    }
}
