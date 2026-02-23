namespace LedgerCore.Domain.Entities

{
    public class Account
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Transaction> Transactions { get; set; } = [];


        // 1. A Chave Estrangeira (o valor no banco)
        public Guid UserId { get; set; }

        // 2. A Propriedade de Navegação (o objeto no C#)
        public virtual User? User { get; set; }
    }
}