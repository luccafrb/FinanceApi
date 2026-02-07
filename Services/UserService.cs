using Microsoft.EntityFrameworkCore;
using FinanceApi.Data;
using FinanceApi.Models;
using FinanceApi.DTOs.Create;
using FinanceApi.DTOs.Responses;

namespace FinanceApi.Services
{
    public class UserService(AppDbContext context) : IUserService
    {
        private readonly AppDbContext _context = context;

        public async Task<User> CreateAsync(UserCreateDto userDto)
        {
            var newUser = new User(userDto.Name, userDto.Email, userDto.Password, userDto.Phone);

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .Select(user => new UserResponseDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone ?? string.Empty,
                    // Remova o ?? e deixe apenas o Select direto
                    Accounts = user.Accounts.Select(a => new AccountSummaryDto
                    {
                        Id = a.Id,
                        Name = a.Name
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .Include(u => u.Accounts)
                .ThenInclude(a => a.Transactions)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Delete(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is not null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Usuário não encontrado para exclusão.");
            }
        }

        public async Task<User> Update(Guid id, UserCreateDto userDto)
        {
            var userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new ArgumentException("Usuário não encontrado com o e-mail informado.");

            userToUpdate.Name = userDto.Name;
            userToUpdate.Phone = userDto.Phone;
            userToUpdate.Email = userDto.Email;

            await _context.SaveChangesAsync();

            return userToUpdate;
        }
    }

}