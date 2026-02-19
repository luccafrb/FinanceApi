namespace LedgerCore.DTOs.Responses
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<CategorySubCategoryResponseDto> SubCategories { get; set; } = [];
    }
}