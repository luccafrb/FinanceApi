using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.DTOs.Create;
using FinanceApi.Models;
using FinanceApi.DTOs.Responses;

namespace FinanceApi.Services.Users
{
    public class AccountService(AppDbContext context) : IAccountService
    {
        private readonly AppDbContext _context = context;

        //Estou realizando o mesmo cáculo de balance no getall e getbyid. então, se alterar em um, tem que alterar no outro.
        public async Task<IEnumerable<AccountResponseDto>> GetAllAsync(Guid userId)
        {
            var accounts = await _context.Accounts
                            .Where(a => a.UserId == userId)
                            .Include(a => a.Transactions)
                            .Select(a => new AccountResponseDto
                            {
                                Name = a.Name,
                                Description = a.Description,
                                Id = a.Id,

                                Balance = a.Transactions.Sum(t => t.Type == TransactionType.Income ? t.Value : -t.Value),
                                Transactions = a.Transactions
                                                .Select(t => new AccountTransactionResponseDto
                                                {
                                                    Id = t.Id,
                                                    Name = t.Name ?? "",
                                                    Description = t.Description ?? "",
                                                    Value = t.Value,
                                                    CompetenceDate = t.CompetenceDate,
                                                    SettlementDate = t.SettlementDate,
                                                    Type = t.Type
                                                })
                            })
                            .ToListAsync();

            return accounts;
        }
        public async Task<Account> CreateAsync(AccountCreateDto accountDto, Guid userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new ArgumentException("Usuário não encontrado com o E-mail informado.");

            var accountNameExists = await _context.Accounts
                .AnyAsync(a => a.UserId == user.Id && a.Name == accountDto.Name);

            if (accountNameExists)
            {
                throw new ArgumentException("Você já possui uma conta com este nome. Tente novamente com outro.");
            }

            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Name = accountDto.Name,
                Description = accountDto.Description,
                UserId = user.Id
            };

            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;
        }
        public async Task<AccountResponseDto> GetByIdAsync(Guid accountId, Guid userId)
        {
            var account = await _context.Accounts
                .AsNoTracking()
                .Where(a => a.Id == accountId && a.UserId == userId)
                .Select(a => new AccountResponseDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description ?? "",
                    Balance = a.Transactions.Sum(t => t.Type == TransactionType.Income ? t.Value : -t.Value),
                    Transactions = a.Transactions.Select(t => new AccountTransactionResponseDto
                    {
                        Id = t.Id,
                        Name = t.Name ?? "",
                        Description = t.Description ?? "",
                        Value = t.Value,
                        CompetenceDate = t.CompetenceDate,
                        SettlementDate = t.SettlementDate,
                        Type = t.Type
                    })
                })
                .FirstOrDefaultAsync()
                ?? throw new ArgumentException("Conta não encontrada com o ID informado.");

            return account;
        }
        public async Task DeleteByIdAsync(Guid id, Guid userId)
        {
            var accountToDelete = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId)
                ?? throw new ArgumentException("Conta não encontrada pelo ID informado.");

            _context.Accounts.Remove(accountToDelete);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateByIdAsync(AccountCreateDto accountDto, Guid accountId, Guid userId)
        {
            var accountToUpdate = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId && a.UserId == userId)
                ?? throw new ArgumentException("Conta não encontrada pelo ID informado.");
            accountToUpdate.Name = accountDto.Name;
            accountToUpdate.Description = accountDto.Description;

            await _context.SaveChangesAsync();
        }
    }
}