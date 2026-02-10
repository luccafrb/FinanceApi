using FinanceApi.Data;
using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Services.Admins
{
    public class AdminService(AppDbContext context) : IAdminService
    {
        private readonly AppDbContext _context = context;

        public async Task PromoteUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new ArgumentException("Usuário não encontrado.");

            user.PromoteToAdmin();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAsync(Guid? userId)
        {
            IQueryable<Account> query = _context.Accounts.AsNoTracking();

            if (userId.HasValue)
                query = query.Where(a => a.UserId == userId);

            return await query.ToListAsync();
        }
    }
}