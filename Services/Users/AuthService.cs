using LedgerCore.Data;
using LedgerCore.DTOs.Create;
using LedgerCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LedgerCore.Services.Users
{
    public class AuthService(AppDbContext context) : IAuthService
    {
        private readonly AppDbContext _context = context;
        public async Task<string> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == userLoginDto.Email)
                ?? throw new ArgumentException("Usuário não encontrado.");

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(userLoginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
                throw new ArgumentException("Senha inválida.");

            var token = TokenService.GenerateToken(user);

            return token;
        }
    }
}