using CarRentalService.Domain.Persons.DTOs.Customers;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Persons.ValueObjects;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers;
using CarRentalService.UseCases.Persons.Customers.Mappers;
using FluentAssertions;
using FluentResults;
using NSubstitute;

namespace UnitTests.UseCases.Persons;

public class CustomerServiceTests
{
    private static Customer testCustomer = new Customer
    {
        Id = Guid.NewGuid(),
        FirstName = "John",
        LastName = "Doe",
        LicenseNumber = "123456",
        PhoneNumber = "123456",
        RegistrationDate = DateTime.Now,
        Address = new Address("Street", "City", "State"),
        DateOfBirth = DateTime.Now
    };
    
    private static CustomerPreviewDto testShowCustomerDto = new CustomerPreviewDto
    {
        Id = testCustomer.Id,
        FullName = testCustomer.FullName,
        LicenseNumber = testCustomer.LicenseNumber,
        PhoneNumber = testCustomer.PhoneNumber,
        RegistrationDate = testCustomer.RegistrationDate,
    };
    
    [Fact]
    public async Task Can_Read_All_Customers()
    {
        //Arrange
        var testCustomers = new List<Customer> { testCustomer };
        var mapper = new CustomerToPreviewDtoMapper();
        var repository = Substitute.For<IRepository<Customer>>();

        repository.GetAllAsync().Returns(testCustomers);
        var sut = new CustomerService(mapper, repository);
        //Act

        var result = await sut.GetAllCustomersAsync();

        //Assert

        result.IsSuccess.Should().BeTrue();
        result.Value.Single().Should().BeEquivalentTo(testShowCustomerDto);
    }
    
    [Fact]
    public async Task Can_Raise_Error_When_Reading_All_Customers()
    {
        //Arrange
        var mapper = Substitute.For<IMapper<Customer, CustomerPreviewDto>>();
        var repository = Substitute.For<IRepository<Customer>>();
        repository.GetAllAsync().Returns(Result.Fail("Error"));
        
        var sut = new CustomerService(mapper, repository);
        //Act

        var result = await sut.GetAllCustomersAsync();

        //Assert
        result.IsFailed.Should().BeTrue();
    }
}