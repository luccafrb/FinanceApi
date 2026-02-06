using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApi.DTOs;
using FinanceApi.Models;

namespace FinanceApi.Services
{
    public interface IAccountService
    {
        public Task<IEnumerable<Account>> GetAll();
        public Task<Account> CreateAsync(AccountCreateDto accountDto);
        public Task<Account> GetById(Guid id);
        public Task DeleteById(Guid id);
        public Task UpdateById(Guid id, AccountCreateDto accountDto);
    }
}