using BankBlazor.API.DTOs;
using BankBlazor.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/Transaction/account/1?pageNumber=1&pageSize=20
        [HttpGet("account/{accountId}")]
        public async Task<ActionResult> GetByAccountId(
            long accountId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20)
        {
            var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId, pageNumber, pageSize);
            var totalCount = await _transactionService.GetTotalTransactionCountAsync(accountId);

            return Ok(new
            {
                transactions,
                pageNumber,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            });
        }
    }
}