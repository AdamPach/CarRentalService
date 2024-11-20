using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Persistence.Common;

namespace CarRentalService.Persistence.Persons;

internal class CustomerRepository(IDataMapper<Customer> dataMapper) : Repository<Customer>(dataMapper)
{
    
}