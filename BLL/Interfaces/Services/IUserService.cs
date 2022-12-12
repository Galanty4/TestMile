using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetAsync(int id);
        Task<UserDto> AddAsync(UserDto userDto);
        Task RemoveAsync(int id);
        Task<UserDto> UpdateAsync(UserDto userDto);
    }
}
