using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;
using FinanceApi.Models;

namespace FinanceApi.Services
{
    public interface IUserService
    {
        public Task<User?> GetByIdAsync(Guid id);
        public Task<User> CreateAsync(UserCreateDto userDto);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        public Task Delete(Guid id);
        public Task<User> Update(Guid id, UserCreateDto userDto);
    }
}