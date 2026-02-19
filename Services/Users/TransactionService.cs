using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Models;
using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;
using System.Runtime.CompilerServices;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace FinanceApi.Services.Users
{
    public class TransactionService(AppDbContext context, IMapper mapper) : ITransactionService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task CreateAsync(TransactionCreateDto dto, Guid userId)
        {
            // 1. Validar se a conta pertence ao usuário
            var accountExists = await _context.Accounts
                .AnyAsync(a => a.Id == dto.AccountId && a.UserId == userId);

            if (!accountExists)
                throw new ArgumentException("Conta não encontrada ou acesso negado.");

            // 2. Validar Categoria (se enviada)
            if (dto.CategoryId.HasValue)
            {
                var categoryExists = await _context.Categories
                    .AnyAsync(c => c.Id == dto.CategoryId && c.UserId == userId);

                if (!categoryExists)
                    throw new ArgumentException("A categoria informada é inválida ou não pertence a este usuário.");
            }

            // 3. Validar Subcategoria (se enviada)
            if (dto.SubCategoryId.HasValue)
            {
                var subCategoryExists = await _context.SubCategories
                    .AnyAsync(s => s.Id == dto.SubCategoryId && s.Category.UserId == userId);

                if (!subCategoryExists)
                    throw new ArgumentException("A subcategoria informada é inválida.");
            }

            // 4. Mapear e Salvar
            var transaction = _mapper.Map<Transaction>(dto);

            // Opcional: Forçar o UserId se sua model de Transaction tiver esse campo direto
            // transaction.UserId = userId; 

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<TransactionResponseDto>> GetAllAsync(TransactionFilterDto transactionFilter, Guid userId)
        {
            var query = _context.Transactions
                .AsNoTracking()
                .Where(t => t.Account != null && t.Account.UserId == userId);

            if (transactionFilter.StartDate.HasValue)
                query = query.Where(t => t.SettlementDate >= transactionFilter.StartDate);

            if (transactionFilter.EndDate.HasValue)
                query = query.Where(t => t.SettlementDate <= transactionFilter.EndDate);

            if (transactionFilter.CategoriesIds != null && transactionFilter.CategoriesIds.Count != 0)
                query = query.Where(t => transactionFilter.CategoriesIds.Contains(t.CategoryId));

            if (transactionFilter.SubCategoriesIds != null && transactionFilter.SubCategoriesIds.Count != 0)
                query = query.Where(t => transactionFilter.SubCategoriesIds.Contains(t.SubCategoryId));

            if (transactionFilter.AccountId.HasValue)
                query = query.Where(t => t.AccountId == transactionFilter.AccountId);

            var transactions = await query.ProjectTo<TransactionResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
            return transactions;
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
        public async Task<TransactionResponseDto> GetByIdAsync(Guid transactionId, Guid userId)
        {
            var transactionToReturn = await _context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == transactionId && t.Account!.UserId == userId)
                ?? throw new ArgumentException("Transação não encontrada.");

            return new TransactionResponseDto()
            {
                Id = transactionToReturn.Id,
                Name = transactionToReturn.Name ?? string.Empty,
                Description = transactionToReturn.Description ?? string.Empty,
                Value = transactionToReturn.Value,
                Type = transactionToReturn.Type,
                CompetenceDate = transactionToReturn.CompetenceDate,
                SettlementDate = transactionToReturn.SettlementDate,
                AccountId = transactionToReturn.AccountId,
                CategoryId = transactionToReturn.CategoryId,
                SubCategoryId = transactionToReturn.SubCategoryId
            };
        }

    }
}