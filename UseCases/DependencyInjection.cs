using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using CarRentalService.UseCases.Persons.Customers.Mappers;
using CarRentalService.UseCases.Persons.Employees;
using CarRentalService.UseCases.Persons.Employees.DTOs;
using CarRentalService.UseCases.Persons.Employees.Mappers;
using CarRentalService.UseCases.Vehicles;
using CarRentalService.UseCases.Vehicles.Cars;
using CarRentalService.UseCases.Vehicles.Cars.DTOs;
using CarRentalService.UseCases.Vehicles.Cars.Mappers;
using CarRentalService.UseCases.Vehicles.Vehicles;
using CarRentalService.UseCases.Vehicles.Vehicles.DTOs;
using CarRentalService.UseCases.Vehicles.Vehicles.Mappers;
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

        hostBuilder.Services.AddScoped<IMapper<Employee, AuthenticatedEmployeeDto>, AuthenticatedEmployeeDtoMapper>();
        hostBuilder.Services.AddScoped<IEmployeeService, EmployeeService>();

        hostBuilder.Services.AddScoped<IVehicleService, VehicleService>();
        hostBuilder.Services.AddScoped<IMapper<Vehicle, VehiclePreviewDto>, VehicleEntityToPreviewDtoMapper>();
        
        hostBuilder.Services.AddScoped<IMapper<CreateCarDto, Car>, CreateCarToEntityMapper>();
        hostBuilder.Services.AddScoped<ICarService, CarService>();
    }
}