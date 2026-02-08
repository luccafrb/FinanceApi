using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;

namespace FinanceApi.Services.Users
{
    public interface ITransactionService
    {
        public Task CreateAsync(TransactionCreateDto transactionCrateDto, Guid userId);
        public Task<IEnumerable<TransactionResponseDto>> GetAllAsync(Guid userId);
    }
}