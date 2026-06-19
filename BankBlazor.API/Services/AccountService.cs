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

        public async Task<(bool Success, string Message, decimal? NewBalance)> DepositAsync(long accountId, decimal amount)
        {
            if (amount <= 0)
                return (false, "Amount must be greater than zero", null);

            var account = await _context.Accounts.FindAsync((int)accountId);

            if (account == null)
                return (false, $"Account {accountId} not found", null);

            account.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = (int)accountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount,
                Balance = account.Balance,
                Type = "Credit",
                Operation = "Deposit"
            });

            await _context.SaveChangesAsync();

            return (true, "Deposit successful", account.Balance);
        }

        public async Task<(bool Success, string Message, decimal? NewBalance)> WithdrawAsync(long accountId, decimal amount)
        {
            if (amount <= 0)
                return (false, "Amount must be greater than zero", null);

            var account = await _context.Accounts.FindAsync((int)accountId);

            if (account == null)
                return (false, $"Account {accountId} not found", null);

            if (account.Balance < amount)
                return (false, "Insufficient funds", account.Balance);

            account.Balance -= amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = (int)accountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Amount = -amount,
                Balance = account.Balance,
                Type = "Debit",
                Operation = "Withdrawal"
            });

            await _context.SaveChangesAsync();

            return (true, "Withdrawal successful", account.Balance);
        }

        public async Task<(bool Success, string Message, decimal? NewBalance)> TransferAsync(long fromAccountId, long toAccountId, decimal amount)
        {
            if (amount <= 0)
                return (false, "Amount must be greater than zero", null);

            if (fromAccountId == toAccountId)
                return (false, "Cannot transfer to the same account", null);

            var fromAccount = await _context.Accounts.FindAsync((int)fromAccountId);
            var toAccount = await _context.Accounts.FindAsync((int)toAccountId);

            if (fromAccount == null)
                return (false, $"Account {fromAccountId} not found", null);

            if (toAccount == null)
                return (false, $"Account {toAccountId} not found", null);

            if (fromAccount.Balance < amount)
                return (false, "Insufficient funds", fromAccount.Balance);

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            _context.Transactions.Add(new Transaction
            {
                AccountId = (int)fromAccountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Amount = -amount,
                Balance = fromAccount.Balance,
                Type = "Debit",
                Operation = $"Transfer to account {toAccountId}"
            });

            _context.Transactions.Add(new Transaction
            {
                AccountId = (int)toAccountId,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount,
                Balance = toAccount.Balance,
                Type = "Credit",
                Operation = $"Transfer from account {fromAccountId}"
            });

            await _context.SaveChangesAsync();

            return (true, "Transfer successful", fromAccount.Balance);
        }
    }
}