using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Persons.ValueObjects;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using CarRentalService.UseCases.Persons.Customers.Mappers;
using CarRentalService.UseCases.Persons.Customers.Repository;
using FluentAssertions;
using FluentResults;
using FluentValidation;
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

    private static CustomerDetailDto testCustomerDetailDto = new CustomerDetailDto()
    {
        Id = testCustomer.Id,
        FirstName = testCustomer.FirstName,
        LastName = testCustomer.LastName,
        DateOfBirth = testCustomer.DateOfBirth,
        Email = testCustomer.Email,
        PhoneNumber = testCustomer.PhoneNumber,
        City = testCustomer.Address?.City,
        Street = testCustomer.Address?.Street,
        ZipCode = testCustomer.Address?.ZipCode,
        LicenseNumber = testCustomer.LicenseNumber,
        RegistrationDate = testCustomer.RegistrationDate
    };
    
    private IMapper<Customer, CustomerPreviewDto> _previewMapper;
    private IMapper<Customer, CustomerDetailDto> _detailMapper;
    private IMapper<CreateCustomerDto, Customer> _createMapper;
    
    private IValidator<CreateCustomerDto> _createValidator;
    
    private readonly ICustomerRepository _repository;
    
    public CustomerServiceTests()
    {
        _previewMapper = Substitute.For<IMapper<Customer, CustomerPreviewDto>>();
        _detailMapper = Substitute.For<IMapper<Customer, CustomerDetailDto>>();
        _createMapper = Substitute.For<IMapper<CreateCustomerDto, Customer>>();
        
        _createValidator = Substitute.For<IValidator<CreateCustomerDto>>();
        
        _repository = Substitute.For<ICustomerRepository>();
    }
    
    [Fact]
    public async Task Can_Read_All_Customers()
    {
        //Arrange
        var testCustomers = new List<Customer> { testCustomer };

        _previewMapper = new CustomerToPreviewDtoMapper();
        _repository.GetAllAsync(CustomerCriteria.Empty).Returns(Result.Ok(testCustomers.AsEnumerable()));
        var sut = new CustomerService(_previewMapper, _detailMapper, _repository, _createMapper, _createValidator);
        
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
        _repository.GetAllAsync(CustomerCriteria.Empty).Returns(Result.Fail("Error"));
        
        var sut = new CustomerService(_previewMapper, _detailMapper, _repository, _createMapper, _createValidator);
        //Act

        var result = await sut.GetAllCustomersAsync();

        //Assert
        result.IsFailed.Should().BeTrue();
    }
    
    [Fact]
    public async Task Can_Read_Customer_By_Id()
    {
        //Arrange
        _repository.GetByIdAsync(testCustomer.Id).Returns(testCustomer);

        _detailMapper = new CustomerToDetailDtoMapper();
        
        var sut = new CustomerService(_previewMapper, _detailMapper, _repository, _createMapper, _createValidator);
        
        //Act
        
        var result = await sut.GetCustomerByIdAsync(testCustomer.Id);
        
        //Assert

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(testCustomerDetailDto);
    }

    [Fact]
    public async Task Can_Raise_Error_When_Not_Found()
    {
        _repository.GetByIdAsync(testCustomer.Id).Returns(Result.Fail("Not found"));
        
        var sut = new CustomerService(_previewMapper, _detailMapper, _repository, _createMapper, _createValidator);
        
        //Act
        
        var result = await sut.GetCustomerByIdAsync(testCustomer.Id);
        
        //Assert
        result.IsFailed.Should().BeTrue();
    }
}