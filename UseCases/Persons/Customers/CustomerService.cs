using CarRentalService.Domain.Persons.DTOs.Customers;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers;

internal class CustomerService : ICustomerService
{
    private readonly IMapper<Customer, CustomerPreviewDto> _customerToDtoMapper;
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(
        IMapper<Customer, CustomerPreviewDto> customerToDtoMapper, 
        IRepository<Customer> customerRepository)
    {
        _customerToDtoMapper = customerToDtoMapper;
        _customerRepository = customerRepository;
    }

    public async Task<Result<IEnumerable<CustomerPreviewDto>>> GetAllCustomersAsync()
    {
        var customersResult = await _customerRepository.GetAllAsync();
        
        if(customersResult.IsFailed)
        {
            return Result.Fail<IEnumerable<CustomerPreviewDto>>(customersResult.Errors);
        }
        var customerDtos = customersResult.Value.Select(c => _customerToDtoMapper.Map(c).Value);
        
        return Result.Ok(customerDtos);
    }
}