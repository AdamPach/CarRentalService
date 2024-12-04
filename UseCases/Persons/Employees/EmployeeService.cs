using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Employees.DTOs;
using CarRentalService.UseCases.Persons.Employees.Repository;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Employees;

internal class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    
    private readonly IMapper<Employee, AuthenticatedEmployeeDto> _authenticatedEmployeeDtoMapper;

    public EmployeeService(
        IEmployeeRepository employeeRepository, 
        IMapper<Employee, AuthenticatedEmployeeDto> authenticatedEmployeeDtoMapper)
    {
        _employeeRepository = employeeRepository;
        _authenticatedEmployeeDtoMapper = authenticatedEmployeeDtoMapper;
    }

    public async Task<Result<AuthenticatedEmployeeDto>> LoginAsync(string employeeNumber, string password)
    {
        var criteria = new EmployeeCriteria
        {
            EmployeeNumber = employeeNumber
        };
        
        var employeeResult = await _employeeRepository.GetAllAsync(criteria);
        
        if (employeeResult.IsFailed)
        {
            return Result.Fail(employeeResult.Errors);
        }
        
        var employee = employeeResult.Value.SingleOrDefault();
        
        if (employee == null)
        {
            return Result.Fail("Employee not found");
        }
        
        if (employee.Password != password)
        {
            return Result.Fail("Invalid password");
        }
        
        var authenticatedEmployeeDtoResult = _authenticatedEmployeeDtoMapper.Map(employee);
        
        return Result.Ok(authenticatedEmployeeDtoResult.Value);
    }
}