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
    }
}