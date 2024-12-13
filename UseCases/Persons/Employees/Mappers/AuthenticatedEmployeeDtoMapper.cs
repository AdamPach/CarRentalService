using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Employees.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Employees.Mappers;

public class AuthenticatedEmployeeDtoMapper : IMapper<Employee, AuthenticatedEmployeeDto>
{
    public Result<AuthenticatedEmployeeDto> Map(Employee from)
    {
        var role = from switch
        {
            ManagerEmployee => AuthenticatedEmployeeDto.EmployeeRole.Manager,
            FullTimeEmployee => AuthenticatedEmployeeDto.EmployeeRole.FullTimeEmployee,
            _ => throw new ArgumentOutOfRangeException(nameof(from))
        };

        return Result.Ok(new AuthenticatedEmployeeDto
        {
            Id = from.Id,
            Username = from.FullName,
            Role = role
        });
    }
}