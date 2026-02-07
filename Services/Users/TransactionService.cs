using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Models;
using FinanceApi.DTOs.Create;

namespace FinanceApi.Services.Users
{
    public class TransactionService(AppDbContext context) : ITransactionService
    {
        private readonly AppDbContext _context = context;
        public async Task CreateAsync(TransactionCreateDto transactionCreateDto)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == transactionCreateDto.AccountId)
                ?? throw new ArgumentException("Nenhuma conta foi encontrada com este ID.");

            var newTransaction = new Transaction()
            {
                Name = transactionCreateDto.Name,
                Description = transactionCreateDto.Description,
                AccountId = transactionCreateDto.AccountId,
                SettlementDate = transactionCreateDto.SettlementDate,
                CompetenceDate = transactionCreateDto.CompetenceDate,
                Type = transactionCreateDto.Type,
                Value = transactionCreateDto.Value,
                CategoryId = transactionCreateDto.CategoryId,
                SubCategoryId = transactionCreateDto.SubCategoryId

            };

            await _context.Transactions.AddAsync(newTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .AsNoTracking()
                .ToListAsync();
        }
    }
}