using BankBlazor.API.Data;
using BankBlazor.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankBlazorContext _context;

        public TransactionService(BankBlazorContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionDto>> GetTransactionsByAccountIdAsync(long accountId, int pageNumber, int pageSize)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TransactionDto
                {
                    TransactionId = t.TransactionId,
                    AccountId = t.AccountId,
                    Date = t.Date,
                    Amount = t.Amount,
                    Balance = t.Balance,
                    Type = t.Type ?? string.Empty,
                    Operation = t.Operation ?? string.Empty
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalTransactionCountAsync(long accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .CountAsync();
        }
    }
}