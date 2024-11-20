using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Persistence.Common;
using CarRentalService.Persistence.Persons;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.Persistence.PostgreSql.DataMappers;
using CarRentalService.Persistence.PostgreSql.Mappers;
using CarRentalService.UseCases.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRentalService.Persistence.PostgreSql;

public static class DependencyInjection
{
    public static void AddPostgreSql(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddSingleton<DatabaseConnectionFactory>();
        
        hostBuilder.Services.AddScoped(typeof(SpecificationToSqlBuilderMapper<>));
        hostBuilder.Services.AddScoped<IDataMapper<Customer>, CustomerDataMapper>();
        hostBuilder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
    }
}