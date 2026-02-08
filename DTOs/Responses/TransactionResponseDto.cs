using FinanceApi.Models;

namespace FinanceApi.DTOs.Responses
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CompetenceDate { get; set; }
        public DateTime LiquidationDate { get; set; }
        public Guid AccountId { get; set; }
        public TransactionType Type { get; set; }
    }
}