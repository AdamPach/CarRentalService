﻿namespace CarRentalService.UseCases.Persons.Employees.DTOs;

public class AuthenticatedEmployeeDto
{
    public enum EmployeeRole
    {
        Manager,
        FullTimeEmployee,
    }
    
    public required string Username { get; set; }
    public required EmployeeRole Role { get; set; }
}