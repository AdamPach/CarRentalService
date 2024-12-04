using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Persistence.PostgreSql.Common;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.Persistence.PostgreSql.Persons.Mappers;
using CarRentalService.Persistence.PostgreSql.Persons.Repositories;
using CarRentalService.Persistence.PostgreSql.Vehicles.Mappers;
using CarRentalService.Persistence.PostgreSql.Vehicles.Repositories;
using CarRentalService.UseCases.Persons.Customers.Repository;
using CarRentalService.UseCases.Persons.Employees.Repository;
using CarRentalService.UseCases.Vehicles.Cars.Repositories;
using CarRentalService.UseCases.Vehicles.Vehicles.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRentalService.Persistence.PostgreSql;

public static class DependencyInjection
{
    public static void AddPostgreSql(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddSingleton<DatabaseConnectionFactory>();

        hostBuilder.Services.AddScoped<ICustomerRepository, PostgreSqlCustomerRepository>();
        hostBuilder.Services.AddScoped<CriteriaToSqlMapper<CustomerCriteria>, CustomerCriteriaToSqlMapper>();
        
        hostBuilder.Services.AddScoped<IEmployeeRepository, PostgreSqlEmployeeRepository>();
        hostBuilder.Services.AddScoped<CriteriaToSqlMapper<EmployeeCriteria>, EmployeeCriteriaToSqlMapper>();
        
        hostBuilder.Services.AddScoped<IVehicleRepository, PostgreSqlVehicleRepository>();
        hostBuilder.Services.AddScoped<CriteriaToSqlMapper<VehicleCriteria>, VehicleCriteriaToSqlMapper>();

        hostBuilder.Services.AddScoped<ICarRepository, PostgreSqlCarRepository>();
    }
}