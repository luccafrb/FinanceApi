using LedgerCore.DTOs.Create;
using LedgerCore.DTOs.Responses;
using LedgerCore.Models;

namespace LedgerCore.Services.Users
{
    public interface IAccountService
    {
        public Task<IEnumerable<AccountResponseDto>> GetAllAsync(Guid userId);
        public Task<Account> CreateAsync(AccountCreateDto accountDto, Guid id);
        public Task<AccountResponseDto> GetByIdAsync(Guid accountId, Guid userId);
        public Task DeleteByIdAsync(Guid accountId, Guid userId);
        public Task UpdateByIdAsync(AccountCreateDto accountDto, Guid accountId, Guid userId);
    }
}