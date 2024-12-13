using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Persistence.JsonFile.FileManager;
using CarRentalService.Persistence.JsonFile.People.Mappers;
using CarRentalService.UseCases.Persons.Customers.Repository;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.People.Repositories;

internal sealed class JsonFileCustomersRepository : ICustomerRepository
{
    private readonly CustomerCriteriaToFilterTypeMapper _customerCriteriaToFilterTypeMapper;
    
    private readonly JsonFileManager<Customer> _customerJsonFileManager;

    public JsonFileCustomersRepository(
        CustomerCriteriaToFilterTypeMapper customerCriteriaToFilterTypeMapper, 
        JsonFileManager<Customer> customerJsonFileManager)
    {
        _customerCriteriaToFilterTypeMapper = customerCriteriaToFilterTypeMapper;
        _customerJsonFileManager = customerJsonFileManager;
    }

    public async Task<Result<IEnumerable<Customer>>> GetAllAsync(CustomerCriteria criteria)
    {
        var fileReadResult = await _customerJsonFileManager.ReadFromFileAsync();
        
        if (fileReadResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Customer>>(fileReadResult.Errors);
        }
        
        var customers = fileReadResult.Value;
        
        var filterType = _customerCriteriaToFilterTypeMapper.Map(criteria).Value;
        
        return Result.Ok(filterType.Apply(customers.AsQueryable()));
    }

    public async Task<Result<Customer>> GetByIdAsync(Guid id)
    {
        var fileReadResult = await _customerJsonFileManager.ReadFromFileAsync();
        
        if (fileReadResult.IsFailed)
        {
            return Result.Fail<Customer>(fileReadResult.Errors);
        }
        
        var customers = fileReadResult.Value.ToList();
        
        return customers.Count switch
        {
            0 => Result.Fail<Customer>("Customer not found"),
            1 => Result.Ok(customers.Single(c => c.Id == id)),
            _ => Result.Fail<Customer>("Multiple customers found")
        };
    }

    public async Task<Result<Customer>> CreateAsync(Customer customer)
    {
        customer.Id = Guid.NewGuid();

        var readResult = await _customerJsonFileManager.ReadFromFileAsync();
        
        if (readResult.IsFailed)
        {
            return Result.Fail<Customer>(readResult.Errors);
        }
        
        var customers = readResult.Value.ToList();
        
        customers.Add(customer);
        
        var writeResult = await _customerJsonFileManager.WriteToFileAsync(customers);
        
        if (writeResult.IsFailed)
        {
            return Result.Fail<Customer>(writeResult.Errors);
        }
        
        return Result.Ok(customer);
    }
}