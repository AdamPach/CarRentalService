using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;

namespace CarRentalService.UseCases.Persons.Customers.Repository;

public interface ICustomerRepository : IReadRepository<Customer, CustomerCriteria>;