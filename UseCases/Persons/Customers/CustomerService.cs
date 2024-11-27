using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Persons.Customers.DTOs;
using CarRentalService.UseCases.Persons.Customers.Repository;
using FluentResults;
using FluentValidation;

namespace CarRentalService.UseCases.Persons.Customers;

internal class CustomerService : ICustomerService
{
    private readonly IMapper<Customer, CustomerPreviewDto> _customerToDtoMapper;
    private readonly IMapper<Customer, CustomerDetailDto> _customerToDetailDtoMapper;
    private readonly IMapper<CreateCustomerDto, Customer> _createCustomerToEntityMapper;
    private readonly IValidator<CreateCustomerDto> _createCustomerValidator;
    
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(
        IMapper<Customer, CustomerPreviewDto> customerToDtoMapper, 
        IMapper<Customer, CustomerDetailDto> customerToDetailDtoMapper, 
        ICustomerRepository customerRepository, 
        IMapper<CreateCustomerDto, Customer> createCustomerToEntityMapper, 
        IValidator<CreateCustomerDto> createCustomerValidator)
    {
        _customerToDtoMapper = customerToDtoMapper;
        _customerToDetailDtoMapper = customerToDetailDtoMapper;
        _customerRepository = customerRepository;
        _createCustomerToEntityMapper = createCustomerToEntityMapper;
        _createCustomerValidator = createCustomerValidator;
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

    public async Task<Result<CustomerDetailDto>> CreateCustomerAsync(CreateCustomerDto customerCreateDto)
    {
        var validationResult = await _createCustomerValidator.ValidateAsync(customerCreateDto);
        
        if(!validationResult.IsValid)
        {
            return Result.Fail("Validation failed");
        }
        
        var mappingResult = _createCustomerToEntityMapper.Map(customerCreateDto);
        
        if(mappingResult.IsFailed)
        {
            return Result.Fail<CustomerDetailDto>(mappingResult.Errors);
        }
        
        var customer = mappingResult.Value;
        
        var createResult = await _customerRepository.CreateAsync(customer);

        return createResult.IsSuccess
            ? Result.Ok()
            : Result.Fail<CustomerDetailDto>(createResult.Errors);
    }
}