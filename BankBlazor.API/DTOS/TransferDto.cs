namespace BankBlazor.API.DTOs
{
    public class TransferDto
    {
        public long FromAccountId { get; set; }
        public long ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}