using CarRentalService.Domain.Persons.DTOs.Customers;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers;

public interface ICustomerService
{
    public Task<Result<IEnumerable<CustomerPreviewDto>>> GetAllCustomersAsync();
}