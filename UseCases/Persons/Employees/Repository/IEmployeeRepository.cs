using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Employees.Repository;

public interface IEmployeeRepository
{
    Task<Result<IEnumerable<Employee>>> GetAllAsync(EmployeeCriteria criteria);
}