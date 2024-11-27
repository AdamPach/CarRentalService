using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using CarRentalService.UseCases.Persons.Customers.Repository;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers;

internal class CustomerService : ICustomerService
{
    private readonly IMapper<Customer, CustomerPreviewDto> _customerToDtoMapper;
    private readonly IMapper<Customer, CustomerDetailDto> _customerToDetailDtoMapper;
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(
        IMapper<Customer, CustomerPreviewDto> customerToDtoMapper, 
        IMapper<Customer, CustomerDetailDto> customerToDetailDtoMapper, 
        ICustomerRepository customerRepository)
    {
        _customerToDtoMapper = customerToDtoMapper;
        _customerToDetailDtoMapper = customerToDetailDtoMapper;
        _customerRepository = customerRepository;
    }

    public async Task<Result<IEnumerable<CustomerPreviewDto>>> GetAllCustomersAsync()
    {
        var customersResult = await _customerRepository.GetAllAsync(CustomerCriteria.Empty);
        
        if(customersResult.IsFailed)
        {
            return Result.Fail<IEnumerable<CustomerPreviewDto>>(customersResult.Errors);
        }
        
        var customerDtos = customersResult.Value.Select(c => _customerToDtoMapper.Map(c).Value);
        
        return Result.Ok(customerDtos);
    }

    public async Task<Result<CustomerDetailDto>> GetCustomerByIdAsync(Guid id)
    {
        var customerResult = await _customerRepository.GetByIdAsync(id);
        
        if(customerResult.IsFailed)
        {
            return Result.Fail<CustomerDetailDto>(customerResult.Errors);
        }
        
        var customerDto = _customerToDetailDtoMapper.Map(customerResult.Value).Value;
        
        return customerDto;
    }
}