using BankBlazor.API.DTOs;

namespace BankBlazor.API.Services
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerByIdAsync(int customerId);
        Task<List<CustomerDto>> GetAllCustomersAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCustomerCountAsync();
    }
}