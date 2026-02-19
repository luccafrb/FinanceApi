using LedgerCore.DTOs.Create;
using LedgerCore.DTOs.Responses;

namespace LedgerCore.Services.Users
{
    public interface ITransactionService
    {
        public Task CreateAsync(TransactionCreateDto transactionCrateDto, Guid userId);
        public Task<IEnumerable<TransactionResponseDto>> GetAllAsync(TransactionFilterDto transactionFilter, Guid userId);
        public Task DeleteByIdAsync(Guid id, Guid userId);
        public Task UpdateByIdAsync(Guid id, TransactionCreateDto transactionCreateDto, Guid userId);
        public Task<TransactionResponseDto> GetByIdAsync(Guid transactionId, Guid userId);
    }
}