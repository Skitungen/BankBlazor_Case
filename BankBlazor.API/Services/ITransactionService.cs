using BankBlazor.API.DTOs;

namespace BankBlazor.API.Services
{
    public interface ITransactionService
    {
        Task<List<TransactionDto>> GetTransactionsByAccountIdAsync(long accountId, int pageNumber, int pageSize);
        Task<int> GetTotalTransactionCountAsync(long accountId);
    }
}