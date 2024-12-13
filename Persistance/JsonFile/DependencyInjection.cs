using CarRentalService.Persistence.JsonFile.FileManager;
using CarRentalService.Persistence.JsonFile.People.Mappers;
using CarRentalService.Persistence.JsonFile.People.Repositories;
using CarRentalService.Persistence.JsonFile.Rentals.Repositories;
using CarRentalService.Persistence.JsonFile.Vehicles.Mappers;
using CarRentalService.Persistence.JsonFile.Vehicles.Repositories;
using CarRentalService.UseCases.Persons.Customers.Repository;
using CarRentalService.UseCases.Persons.Employees.Repository;
using CarRentalService.UseCases.Rentals.Repositories;
using CarRentalService.UseCases.Vehicles.Cars.Repositories;
using CarRentalService.UseCases.Vehicles.Vehicles.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRentalService.Persistence.JsonFile;

public static class DependencyInjection
{
    public static void AddJsonFile(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddSingleton(typeof(JsonFileManager<>));

        hostBuilder.Services.AddScoped<IEmployeeRepository, JsonFileEmployeeRepository>();
        hostBuilder.Services.AddTransient<EmployeeCriteriaToFilterTypeMapper>();
        
        hostBuilder.Services.AddScoped<ICustomerRepository, JsonFileCustomersRepository>();
        hostBuilder.Services.AddTransient<CustomerCriteriaToFilterTypeMapper>();
        
        hostBuilder.Services.AddScoped<IVehicleRepository, JsonFileVehicleRepository>();
        hostBuilder.Services.AddTransient<VehicleCriteriaToFilterTypeMapper>();
        
        hostBuilder.Services.AddScoped<ICarRepository, JsonFileCarRepository>();
        hostBuilder.Services.AddScoped<IRentalRepository, JsonFileRentalRepository>();
    }
}