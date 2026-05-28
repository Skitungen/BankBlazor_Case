using BankBlazor.API.DTOs;

namespace BankBlazor.API.Services
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAccountsByCustomerIdAsync(int customerId);
        Task<AccountDto?> GetAccountByIdAsync(long accountId);
    }
}