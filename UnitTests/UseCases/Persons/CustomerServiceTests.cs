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
using UnitTests.Fakers.Persons.Customers;

namespace UnitTests.UseCases.Persons;

public class CustomerServiceTests
{
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
        var testCustomers = CustomerData
            .ValidCustomerFaker
            .Generate(20);

        _previewMapper = new CustomerToPreviewDtoMapper();
        _repository.GetAllAsync(CustomerCriteria.Empty).Returns(testCustomers);
        var sut = new CustomerService(_previewMapper, _detailMapper, _repository, _createMapper, _createValidator);
        
        //Act
        var result = await sut.GetAllCustomersAsync();

        //Assert
        result.IsSuccess
            .Should()
            .BeTrue();
        result.Value
            .Should()
            .BeEquivalentTo(CustomerData.CustomersPreview(testCustomers));
    }
    
    [Fact]
    public async Task Can_Raise_Error_When_Reading_All_Customers()
    {
        //Arrange
        _repository.GetAllAsync(CustomerCriteria.Empty)
            .Returns(Result.Fail("Error"));
        
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
        var testCustomer = CustomerData.ValidCustomerFaker.Generate();
        
         _repository.GetByIdAsync(testCustomer.Id).Returns(testCustomer);

        _detailMapper = new CustomerToDetailDtoMapper();
        
        var sut = new CustomerService(_previewMapper, _detailMapper, _repository, _createMapper, _createValidator);
        
        //Act
        
        var result = await sut.GetCustomerByIdAsync(testCustomer.Id);
        
        //Assert

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(CustomerData.CustomersDetail(testCustomer).First());
    }

    [Fact]
    public async Task Can_Raise_Error_When_Not_Found()
    {
        //Arrange
        var testCustomer = CustomerData.ValidCustomerFaker.Generate();
        _repository.GetByIdAsync(testCustomer.Id).Returns(Result.Fail("Not found"));
        
        var sut = new CustomerService(_previewMapper, _detailMapper, _repository, _createMapper, _createValidator);
        
        //Act
        
        var result = await sut.GetCustomerByIdAsync(testCustomer.Id);
        
        //Assert
        result.IsFailed.Should().BeTrue();
    }
}