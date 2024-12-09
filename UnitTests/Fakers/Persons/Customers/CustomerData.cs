using Bogus;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Persons.ValueObjects;
using CarRentalService.UseCases.Persons.Customers.DTOs;

namespace UnitTests.Fakers.Persons.Customers;

public static class CustomerData
{
    public static readonly Faker<Customer> ValidCustomerFaker;
    
    public static IEnumerable<CustomerPreviewDto> CustomersPreview(params IEnumerable<Customer> customers) =>
        customers.Select(c => new CustomerPreviewDto
        {
            Id = c.Id,
            FullName = c.FullName,
            LicenseNumber = c.LicenseNumber,
            PhoneNumber = c.PhoneNumber,
            RegistrationDate = c.RegistrationDate
        }).ToList();
    
    public static IEnumerable<CustomerDetailDto> CustomersDetail(params IEnumerable<Customer> customers) =>
        customers.Select(c => new CustomerDetailDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            DateOfBirth = c.DateOfBirth,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            City = c.Address?.City,
            Street = c.Address?.Street,
            ZipCode = c.Address?.ZipCode,
            LicenseNumber = c.LicenseNumber,
            RegistrationDate = c.RegistrationDate
        }).ToList();
    
    static CustomerData()
    {
        ValidCustomerFaker = new Faker<Customer>();
        ValidCustomerFaker.RuleFor(c => c.Id, f => f.Random.Guid());
        ValidCustomerFaker.RuleFor(c => c.FirstName, f => f.Person.FirstName);
        ValidCustomerFaker.RuleFor(c => c.LastName, f => f.Person.LastName);
        ValidCustomerFaker.RuleFor(c => c.LicenseNumber, f => f.Random.String2(6));
        ValidCustomerFaker.RuleFor(c => c.PhoneNumber, f => f.Person.Phone);
        ValidCustomerFaker.RuleFor(c => c.RegistrationDate, f => f.Date.Past());
        ValidCustomerFaker.RuleFor(c => c.DateOfBirth, f => f.Person.DateOfBirth);
        ValidCustomerFaker.RuleFor(c => c.Email, f => f.Random.Bool() ? f.Person.Email : null);
        ValidCustomerFaker.RuleFor(c => c.Address, f => 
            new Address(f.Address.StreetName(), f.Address.City(), f.Address.State()));
        
    }
}