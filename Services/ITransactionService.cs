
using FinanceApi.DTOs;
using FinanceApi.Models;

namespace FinanceApi.Services
{
    public interface ITransactionService
    {
        public Task CreateAsync(TransactionCreateDto transactionCrateDto);
        public Task<IEnumerable<Transaction>> GetAllAsync();
    }
}