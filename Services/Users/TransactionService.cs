using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Models;
using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;

namespace FinanceApi.Services.Users
{
    public class TransactionService(AppDbContext context) : ITransactionService
    {
        private readonly AppDbContext _context = context;
        public async Task CreateAsync(TransactionCreateDto transactionCreateDto, Guid userId)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == transactionCreateDto.AccountId && a.UserId == userId)
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
                SubCategoryId = transactionCreateDto.SubCategoryId,

                Account = account
            };

            await _context.Transactions.AddAsync(newTransaction);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync(Guid userId)
        {
            return await _context.Transactions
                .Include(t => t.Account)
                .AsNoTracking()
                .Where(t => t.Account != null && t.Account.UserId == userId)
                .Select(t => new TransactionResponseDto
                {
                    Id = t.Id,
                    Name = t.Name ?? "",
                    Description = t.Description ?? "",
                    Value = t.Value,
                    CompetenceDate = t.CompetenceDate,
                    SettlementDate = t.SettlementDate,
                    AccountId = t.AccountId,
                    Type = t.Type
                })
                .ToListAsync();
        }
    }
}