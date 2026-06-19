namespace BankBlazor.API.DTOs
{
    public class DepositWithdrawDto
    {
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}