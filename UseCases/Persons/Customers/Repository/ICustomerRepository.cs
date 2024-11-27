using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers.Repository;

public interface ICustomerRepository : IReadRepository<Customer, CustomerCriteria>
{
    Task<Result<Customer>> CreateAsync(Customer customer);
}