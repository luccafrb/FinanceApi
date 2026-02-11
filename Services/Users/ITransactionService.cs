using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;

namespace FinanceApi.Services.Users
{
    public interface ITransactionService
    {
        public Task CreateAsync(TransactionCreateDto transactionCrateDto, Guid userId);
        public Task<IEnumerable<TransactionResponseDto>> GetAllAsync(Guid userId);
        public Task DeleteByIdAsync(Guid id, Guid userId);
        public Task UpdateByIdAsync(Guid id, TransactionCreateDto transactionCreateDto, Guid userId);
        public Task<TransactionResponseDto> GetByIdAsync(Guid transactionId, Guid userId);
    }
}