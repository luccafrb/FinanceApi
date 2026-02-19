namespace LedgerCore.Models
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Inicializar a lista evita erros ao dar um .Add() futuramente
        public virtual ICollection<SubCategory> SubCategories { get; set; } = [];
    }
}