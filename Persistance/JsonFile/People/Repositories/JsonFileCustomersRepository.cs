using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Persons.Customers.Repository;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.People.Repositories;

public class JsonFileCustomersRepository : ICustomerRepository
{
    public Task<Result<IEnumerable<Customer>>> GetAllAsync(CustomerCriteria criteria)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Customer>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Customer>> CreateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}