using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using CarRentalService.UseCases.Persons.Customers.Mappers;
using CarRentalService.UseCases.Persons.Employees;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRentalService.UseCases;

public static class DependencyInjection
{
    public static void AddUseCases(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        hostBuilder.Services.AddScoped<IMapper<CreateCustomerDto, Customer>, CreateCustomerToEntity>();
        hostBuilder.Services.AddScoped<IMapper<Customer, CustomerDetailDto>, CustomerToDetailDtoMapper>();
        hostBuilder.Services.AddScoped<IMapper<Customer, CustomerPreviewDto>, CustomerToPreviewDtoMapper>();
        hostBuilder.Services.AddScoped<ICustomerService, CustomerService>();

        hostBuilder.Services.AddScoped<IEmployeeService, EmployeeService>();
    }
}