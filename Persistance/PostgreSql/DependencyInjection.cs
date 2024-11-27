using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Persistence.PostgreSql.Common;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.Persistence.PostgreSql.Persons.Mappers;
using CarRentalService.Persistence.PostgreSql.Persons.Repositories;
using CarRentalService.UseCases.Persons.Customers.Repository;
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
    }
}