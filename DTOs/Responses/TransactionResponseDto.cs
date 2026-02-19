using LedgerCore.Models;

namespace LedgerCore.DTOs.Responses
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTime CompetenceDate { get; set; }
        public DateTime? SettlementDate { get; set; }
        public Guid AccountId { get; set; }
        public TransactionType Type { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
    }
}