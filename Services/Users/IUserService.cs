using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;
using FinanceApi.Models;

namespace FinanceApi.Services.Users
{
    public interface IUserService
    {
        public Task<User?> GetByIdAsync(Guid id);
        public Task<User> CreateAsync(UserCreateDto userDto);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        public Task DeleteAsync(Guid id);
        public Task<User> UpdateAsync(Guid id, UserCreateDto userDto);
    }
}