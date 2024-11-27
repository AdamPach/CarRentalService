using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;

namespace CarRentalService.UseCases.Persons.Employees.Repository;

public interface IEmployeeRepository : IReadRepository<Employee, EmployeeCriteria>;