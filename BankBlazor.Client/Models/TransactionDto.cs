namespace BankBlazor.Client.Models
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public long AccountId { get; set; }
        public DateOnly Date { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
    }
}