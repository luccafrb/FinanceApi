using System.ComponentModel.DataAnnotations;

namespace FinanceApi.Models
{
    public class Transaction
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AccountId { get; set; }
        public virtual Account? Account { get; set; }

        public TransactionType Type { get; set; }
        public string? Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string? Description { get; set; }

        //Datas
        public DateTime CompetenceDate { get; set; }
        public DateTime? SettlementDate { get; set; }


        // Relacionamentos
        public Guid? SubCategoryId { get; set; }
        // Propriedade de Navegação (O EF usa isso para fazer o JOIN)
        public virtual SubCategory? SubCategory { get; set; }

        public Guid? CategoryId { get; set; }
        public virtual Category? Category { get; set; }

    }
}