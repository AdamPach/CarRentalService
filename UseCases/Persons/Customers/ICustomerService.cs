using CarRentalService.UseCases.Persons.Customers.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers;

public interface ICustomerService
{
    public Task<Result<IEnumerable<CustomerPreviewDto>>> GetAllCustomersAsync();
    
    public Task<Result<CustomerDetailDto>> GetCustomerByIdAsync(Guid id);
}