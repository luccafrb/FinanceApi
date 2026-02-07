using FinanceApi.DTOs.Create;
using FinanceApi.Models;

namespace FinanceApi.Services.Users
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAll(Guid userId);
        public Task<Account> CreateAsync(AccountCreateDto accountDto);
        public Task<Account> GetById(Guid id);
        public Task DeleteById(Guid id);
        public Task UpdateById(Guid id, AccountCreateDto accountDto);
    }
}