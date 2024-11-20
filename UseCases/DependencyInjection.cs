using CarRentalService.Domain.Persons.DTOs.Customers;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers;
using CarRentalService.UseCases.Persons.Customers.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CarRentalService.UseCases;

public static class DependencyInjection
{
    public static void AddUseCases(this IHostApplicationBuilder hostBuilder)
    {
        hostBuilder.Services.AddScoped<IMapper<Customer, CustomerPreviewDto>, CustomerToPreviewDtoMapper>();
        hostBuilder.Services.AddScoped<ICustomerService, CustomerService>();
    }
}