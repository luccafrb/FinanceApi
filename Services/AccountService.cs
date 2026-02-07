using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.DTOs.Create;
using FinanceApi.Models;

namespace FinanceApi.Services
{
    public class AccountService(AppDbContext context) : IAccountService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Account>> GetAll(Guid userId)
        {
            var accounts = await _context.Accounts
                            .Where(a => a.UserId == userId)
                            .ToListAsync();

            return accounts;
        }

        public async Task<Account> CreateAsync(AccountCreateDto accountDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == accountDto.UserId)
                ?? throw new ArgumentException("Usuário não encontrado com o E-mail informado.");

            var accountExists = await _context.Accounts
                .AnyAsync(a => a.UserId == user.Id && a.Name == accountDto.Name);

            if (accountExists)
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
        public async Task<Account> GetById(Guid id)
        {
            var account = await _context.Accounts
                .Include(a => a.Transactions)
                .FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new ArgumentException("Conta não encontrada com o ID informado.");

            return account;
        }
        public async Task DeleteById(Guid id)
        {
            var accountToDelete = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new ArgumentException("Conta não encontrada pelo ID informado.");

            _context.Accounts.Remove(accountToDelete);
        }
        public async Task UpdateById(Guid id, AccountCreateDto accountDto)
        {
            var accountToUpdate = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new ArgumentException("Conta não encontrada pelo ID informado.");
            accountToUpdate.Name = accountDto.Name;
            accountToUpdate.Description = accountDto.Description;

            await _context.SaveChangesAsync();
        }
    }
}