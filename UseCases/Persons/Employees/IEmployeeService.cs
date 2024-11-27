using CarRentalService.Domain.Persons.Entities;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Employees;

public interface IEmployeeService
{
    Task<Result> LoginAsync(string employeeNumber, string password);
}