using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers.Mappers;

public class CustomerToPreviewDtoMapper : IMapper<Customer, CustomerPreviewDto>
{
    public Result<CustomerPreviewDto> Map(Customer from)
    {
        var customerPreviewDto = new CustomerPreviewDto
        {
            Id = from.Id,
            FullName = from.FullName,
            RegistrationDate = from.RegistrationDate,
            LicenseNumber = from.LicenseNumber,
            PhoneNumber = from.PhoneNumber
        };

        return customerPreviewDto;
    }
}