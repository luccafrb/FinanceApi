using FinanceApi.Models;

namespace FinanceApi.DTOs.Create
{
    public class TransactionCreateDto
    {
        public Guid AccountId { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TransactionType Type { get; set; }
        public DateTime CompetenceDate { get; set; }
        public DateTime? SettlementDate { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
    }
}