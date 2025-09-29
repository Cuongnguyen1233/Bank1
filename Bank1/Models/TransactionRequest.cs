namespace Bank1.Models
{
    public class TransactionRequest
    {
        public string AccountName { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = string.Empty; // "deposit" or "withdraw"
    }
}
