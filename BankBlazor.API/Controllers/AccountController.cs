using BankBlazor.API.DTOs;
using BankBlazor.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // GET: api/Account/customer/1
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<AccountDto>>> GetByCustomerId(int customerId)
        {
            var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);

            if (accounts == null || accounts.Count == 0)
                return NotFound($"No accounts found for customer {customerId}");

            return Ok(accounts);
        }

        // GET: api/Account/5
        [HttpGet("{accountId}")]
        public async Task<ActionResult<AccountDto>> GetById(long accountId)
        {
            var account = await _accountService.GetAccountByIdAsync(accountId);

            if (account == null)
                return NotFound($"Account {accountId} not found");

            return Ok(account);
        }

        // POST: api/Account/deposit
        [HttpPost("deposit")]
        public async Task<ActionResult> Deposit(DepositWithdrawDto dto)
        {
            var result = await _accountService.DepositAsync(dto.AccountId, dto.Amount);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message, newBalance = result.NewBalance });
        }

        // POST: api/Account/withdraw
        [HttpPost("withdraw")]
        public async Task<ActionResult> Withdraw(DepositWithdrawDto dto)
        {
            var result = await _accountService.WithdrawAsync(dto.AccountId, dto.Amount);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message, newBalance = result.NewBalance });
        }

        // POST: api/Account/transfer
        [HttpPost("transfer")]
        public async Task<ActionResult> Transfer(TransferDto dto)
        {
            var result = await _accountService.TransferAsync(dto.FromAccountId, dto.ToAccountId, dto.Amount);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(new { message = result.Message, newBalance = result.NewBalance });
        }
    }
}