using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Persons.ValueObjects;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers.Mappers;

public class CreateCustomerToEntity : IMapper<CreateCustomerDto, Customer>
{
    public Result<Customer> Map(CreateCustomerDto from)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            FirstName = from.FirstName,
            LastName = from.LastName,
            Email = from.Email,
            PhoneNumber = from.PhoneNumber,
            Address = new Address(
                from.Street,
                from.City,
                from.ZipCode
            ),
            LicenseNumber = from.LicenseNumber,
            RegistrationDate = DateTime.Now,
            DateOfBirth = from.DateOfBirth!.Value,
        };

        return Result.Ok(customer);
    }
}