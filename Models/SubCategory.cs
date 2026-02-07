namespace FinanceApi.Models
{
    public class SubCategory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Active { get; set; } = true; // Geralmente começamos como True

        // Relacionamento com o Pai (Categoria)
        public Guid CategoryId { get; set; }

        // Propriedade de navegação inversa (Ajuda o EF e suas queries)
        public virtual Category? Category { get; set; }

        // Relacionamento com o Dono (Usuário)
        public Guid UserId { get; set; }
    }
}