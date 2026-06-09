using BankBlazor.API.DTOs;
using BankBlazor.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer/1
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetById(int customerId)
        {
            var customer = await _customerService.GetCustomerByIdAsync(customerId);

            if (customer == null)
                return NotFound($"Customer {customerId} not found");

            return Ok(customer);
        }

        // GET: api/Customer?pageNumber=1&pageSize=50
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
        {
            var customers = await _customerService.GetAllCustomersAsync(pageNumber, pageSize);
            var totalCount = await _customerService.GetTotalCustomerCountAsync();

            return Ok(new
            {
                customers,
                pageNumber,
                pageSize,
                totalCount,
                totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            });
        }
    }
}