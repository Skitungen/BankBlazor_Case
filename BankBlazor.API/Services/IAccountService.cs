using BankBlazor.API.DTOs;

namespace BankBlazor.API.Services
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAccountsByCustomerIdAsync(int customerId);
        Task<AccountDto?> GetAccountByIdAsync(long accountId);
        Task<(bool Success, string Message, decimal? NewBalance)> DepositAsync(long accountId, decimal amount);
        Task<(bool Success, string Message, decimal? NewBalance)> WithdrawAsync(long accountId, decimal amount);
        Task<(bool Success, string Message, decimal? NewBalance)> TransferAsync(long fromAccountId, long toAccountId, decimal amount);
    }
}