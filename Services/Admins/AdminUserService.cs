using LedgerCore.Data;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Services.Admins
{
    public class AdminUserService(AppDbContext context) : IAdminUserService
    {
        private readonly AppDbContext _context = context;
        public async Task PromoteUserAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId)
                ?? throw new ArgumentException("Usuário não encontrado.");

            user.PromoteToAdmin();
            await _context.SaveChangesAsync();
        }
    }
}