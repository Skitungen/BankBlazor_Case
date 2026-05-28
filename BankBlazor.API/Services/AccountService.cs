using BankBlazor.API.Data;
using BankBlazor.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankBlazorContext _context;

        public AccountService(BankBlazorContext context)
        {
            _context = context;
        }

        public async Task<List<AccountDto>> GetAccountsByCustomerIdAsync(int customerId)
        {
            return await _context.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Select(d => new AccountDto
                {
                    AccountId = d.Account.AccountId,
                    Frequency = d.Account.Frequency,
                    Created = d.Account.Created,
                    Balance = d.Account.Balance,
                    AccountType = d.Type
                })
                .ToListAsync();
        }

        public async Task<AccountDto?> GetAccountByIdAsync(long accountId)
        {
            return await _context.Accounts
                .Where(a => a.AccountId == accountId)
                .Select(a => new AccountDto
                {
                    AccountId = a.AccountId,
                    Frequency = a.Frequency,
                    Created = a.Created,
                    Balance = a.Balance,
                    AccountType = a.Frequency
                })
                .FirstOrDefaultAsync();
        }
    }
}