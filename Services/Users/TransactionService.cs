using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Models;
using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;
using System.Runtime.CompilerServices;

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
        public async Task DeleteByIdAsync(Guid id, Guid userId)
        {
            var transactionToRemove = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.Account!.UserId == userId)
                ?? throw new ArgumentException("Transaction não encontrada com o Id informado.");

            _context.Transactions.Remove(transactionToRemove);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateByIdAsync(Guid id, TransactionCreateDto transactionCreateDto, Guid userId)
        {
            var transactionToUpdate = await _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefaultAsync(t => t.Id == id && t.Account!.UserId == userId)
                ?? throw new ArgumentException("Transação não encontrada.");

            var category = _context.Categories
                .AnyAsync(c => c.Id == transactionCreateDto.CategoryId && c.UserId == userId);
            var subCategory = _context.Categories
                .AnyAsync(s => s.Id == transactionCreateDto.SubCategoryId && s.UserId == userId);
            var account = _context.Accounts
                .AnyAsync(a => a.Id == transactionCreateDto.AccountId && a.UserId == userId);

            await Task.WhenAll(category, subCategory, account);

            if (!await category)
                throw new ArgumentException("Categoria não encontrada.");
            if (!await subCategory)
                throw new ArgumentException("Subcategoria não encontrada.");
            if (!await account)
                throw new ArgumentException("Conta não encontrada.");

            transactionToUpdate.Name = transactionCreateDto.Name;
            transactionToUpdate.Description = transactionCreateDto.Description;
            transactionToUpdate.Value = transactionCreateDto.Value;
            transactionToUpdate.Type = transactionCreateDto.Type;
            transactionToUpdate.CompetenceDate = transactionCreateDto.CompetenceDate;
            transactionToUpdate.SettlementDate = transactionCreateDto.SettlementDate;
            transactionToUpdate.CategoryId = transactionCreateDto.CategoryId;
            transactionToUpdate.SubCategoryId = transactionCreateDto.SubCategoryId;
            transactionToUpdate.AccountId = transactionCreateDto.AccountId;

            await _context.SaveChangesAsync();
        }
    }
}