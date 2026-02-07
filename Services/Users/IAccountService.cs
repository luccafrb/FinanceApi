using FinanceApi.DTOs.Create;
using FinanceApi.Models;

namespace FinanceApi.Services.Users
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAllAsync(Guid userId);
        public Task<Account> CreateAsync(AccountCreateDto accountDto, Guid id);
        public Task<Account> GetByIdAsync(Guid accountId, Guid userId);
        public Task DeleteByIdAsync(Guid accountId, Guid userId);
        public Task UpdateByIdAsync(AccountCreateDto accountDto, Guid accountId, Guid userId);
    }
}