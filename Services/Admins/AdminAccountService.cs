using LedgerCore.Data;
using LedgerCore.Models;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Services.Admins
{
    public class AdminAccountService(AppDbContext context) : IAdminAccountService
    {
        private readonly AppDbContext _context = context;

        public async Task<IEnumerable<Account>> GetAllAsync(Guid? userId)
        {
            IQueryable<Account> query = _context.Accounts.AsNoTracking();

            if (userId.HasValue)
                query = query.Where(a => a.UserId == userId);

            return await query.ToListAsync();
        }
    }
}