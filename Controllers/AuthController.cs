using FinanceApi.Data;
using FinanceApi.DTOs;
using FinanceApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null)
                return Unauthorized("E-mail inválido.");

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!isPasswordValid)
                return Unauthorized("Senha inválida.");

            var token = TokenService.GenerateToken(user);

            return Ok(new { token });
        }
    }
}