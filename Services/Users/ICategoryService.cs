using LedgerCore.DTOs.Create;
using LedgerCore.DTOs.Responses;

namespace LedgerCore.Services.Users
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryResponseDto>> GetAllAsync(Guid userId);
        public Task<CategoryResponseDto> GetByIdAsync(Guid categoryId, Guid userId);
        public Task CreateAsync(CategoryCreateDto categoryCreateDto, Guid userId);
        public Task UpdateByIdAsync(Guid categoryId, CategoryCreateDto categoryCreateDto, Guid userId);
        public Task DeleteByIdAsync(Guid categoryId, Guid userIs);
    }
}