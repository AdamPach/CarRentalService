using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Persons.Customers;

internal class CustomerService : ICustomerService
{
    private readonly IMapper<Customer, CustomerPreviewDto> _customerToDtoMapper;
    private readonly IMapper<Customer, CustomerDetailDto> _customerToDetailDtoMapper;
    private readonly IRepository<Customer> _customerRepository;

    public CustomerService(
        IMapper<Customer, CustomerPreviewDto> customerToDtoMapper, 
        IRepository<Customer> customerRepository, 
        IMapper<Customer, CustomerDetailDto> customerToDetailDtoMapper)
    {
        _customerToDtoMapper = customerToDtoMapper;
        _customerRepository = customerRepository;
        _customerToDetailDtoMapper = customerToDetailDtoMapper;
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