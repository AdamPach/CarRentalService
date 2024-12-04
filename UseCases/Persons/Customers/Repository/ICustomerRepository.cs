using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers.Repository;

public interface ICustomerRepository
{
    Task<Result<IEnumerable<Customer>>> GetAllAsync(CustomerCriteria criteria);
    Task<Result<Customer>> GetByIdAsync(Guid id);
    Task<Result<Customer>> CreateAsync(Customer customer);
}