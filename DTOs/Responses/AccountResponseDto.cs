namespace LedgerCore.DTOs.Responses
{
    public class AccountResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public IEnumerable<AccountTransactionResponseDto> Transactions { get; set; } = [];
    }
}