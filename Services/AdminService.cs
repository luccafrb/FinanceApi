using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Services
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
    }
}