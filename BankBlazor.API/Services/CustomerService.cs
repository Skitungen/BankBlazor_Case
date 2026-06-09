using BankBlazor.API.Data;
using BankBlazor.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BankBlazor.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankBlazorContext _context;

        public CustomerService(BankBlazorContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(int customerId)
        {
            return await _context.Customers
                .Where(c => c.CustomerId == customerId)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    FirstName = c.Givenname,
                    LastName = c.Surname,
                    EmailAddress = c.Emailaddress,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    Country = c.Country,
                    NationalId = c.NationalId,
                    Telephonenumber = c.Telephonenumber ?? string.Empty 
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync(int pageNumber, int pageSize)
        {
            return await _context.Customers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    FirstName = c.Givenname,
                    LastName = c.Surname,
                    EmailAddress = c.Emailaddress,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    Country = c.Country,
                    NationalId = c.NationalId,
                    Telephonenumber = c.Telephonenumber ?? string.Empty
                })
                .ToListAsync();
        }

        public async Task<int> GetTotalCustomerCountAsync()
        {
            return await _context.Customers.CountAsync();
        }
    }
}