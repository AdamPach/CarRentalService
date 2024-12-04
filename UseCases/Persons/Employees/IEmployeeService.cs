using CarRentalService.UseCases.Persons.Employees.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Employees;

public interface IEmployeeService
{
    Task<Result<AuthenticatedEmployeeDto>> LoginAsync(string employeeNumber, string password);
}