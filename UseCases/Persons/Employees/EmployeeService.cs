using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Persons.Employees.Repository;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Employees;

internal class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Result> LoginAsync(string employeeNumber, string password)
    {
        var criteria = new EmployeeCriteria
        {
            EmployeeNumber = employeeNumber
        };
        
        var employeeResult = await _employeeRepository.GetAllAsync(criteria);
        
        if (employeeResult.IsFailed)
        {
            return Result.Fail("Employee not found");
        }
        
        var employee = employeeResult.Value.Single();
        
        if (employee.Password != password)
        {
            return Result.Fail("Invalid password");
        }
        
        return Result.Ok();
    }
}