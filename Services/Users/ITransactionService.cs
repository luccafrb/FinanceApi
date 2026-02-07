using FinanceApi.DTOs.Create;
using FinanceApi.Models;

namespace FinanceApi.Services.Users
{
    public interface ITransactionService
    {
        public Task CreateAsync(TransactionCreateDto transactionCrateDto);
        public Task<IEnumerable<Transaction>> GetAllAsync();
    }
}