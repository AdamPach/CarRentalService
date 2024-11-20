using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers.Mappers;

public class CustomerToDetailDtoMapper : IMapper<Customer, CustomerDetailDto>
{
    public Result<CustomerDetailDto> Map(Customer from)
    {
        var customerDetailDto = new CustomerDetailDto
        {
            Id = from.Id,
            FirstName = from.FirstName,
            LastName = from.LastName,
            RegistrationDate = from.RegistrationDate,
            LicenseNumber = from.LicenseNumber,
            Email = from.Email,
            DateOfBirth = from.DateOfBirth,
            PhoneNumber = from.PhoneNumber,
            City = from.Address?.City,
            Street = from.Address?.Street,
            ZipCode = from.Address?.ZipCode
        };

        return customerDetailDto;
    }
}