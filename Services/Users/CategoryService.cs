using AutoMapper;
using LedgerCore.Data;
using LedgerCore.DTOs.Create;
using LedgerCore.DTOs.Responses;
using LedgerCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LedgerCore.Services.Users
{
    public class CategoryService(AppDbContext context, IMapper mapper) : ICategoryService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync(Guid userId)
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => new CategoryResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    SubCategories = c.SubCategories
                        .Select(s => new CategorySubCategoryResponseDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Description = s.Description,
                        })
                        .ToList()
                })
                .ToListAsync();

            await _context.SaveChangesAsync();

            return categories;
        }
        public async Task<CategoryResponseDto> GetByIdAsync(Guid categoryId, Guid userId)
        {
            var category = await _context.Categories
                .AsNoTracking()
                .Where(c => c.Id == categoryId && c.UserId == userId)
                .Select(c => new CategoryResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                })
                .FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            await _context.SaveChangesAsync();

            return category;
        }
        public async Task CreateAsync(CategoryCreateDto categoryCreateDto, Guid userId)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryCreateDto.Name,
                Description = categoryCreateDto.Description,
                UserId = userId,
            };

            await _context.Categories
                .AddAsync(category);

            await _context.SaveChangesAsync();
        }
        public async Task UpdateByIdAsync(Guid categoryId, CategoryCreateDto categoryCreateDto, Guid userId)
        {
            var categoryToUpdate = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId)
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            _mapper.Map(categoryCreateDto, categoryToUpdate);

            await _context.SaveChangesAsync();
        }
        public async Task DeleteByIdAsync(Guid categoyId, Guid userId)
        {
            var accountToDelete = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == categoyId && c.UserId == userId)
                ?? throw new KeyNotFoundException("Categoria não encontrada.");

            _context.Categories.Remove(accountToDelete);

            await _context.SaveChangesAsync();
        }
    }
}