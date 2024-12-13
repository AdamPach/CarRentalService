using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Persistence.JsonFile.FileManager;
using CarRentalService.Persistence.JsonFile.People.Mappers;
using CarRentalService.UseCases.Persons.Employees.Repository;
using CarRentalService.UseCases.Rentals.DTOs;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.People.Repositories;

internal sealed class JsonFileEmployeeRepository : IEmployeeRepository
{
    private readonly JsonFileManager<ManagerEmployee> _managerEmployeeJsonFileManager;
    private readonly JsonFileManager<FullTimeEmployee> _regularEmployeeJsonFileManager;
    
    private readonly EmployeeCriteriaToFilterTypeMapper _employeeCriteriaToFilterTypeMapper;

    public JsonFileEmployeeRepository(
        JsonFileManager<ManagerEmployee> managerEmployeeJsonFileManager, 
        JsonFileManager<FullTimeEmployee> regularEmployeeJsonFileManager, 
        EmployeeCriteriaToFilterTypeMapper employeeCriteriaToFilterTypeMapper)
    {
        _managerEmployeeJsonFileManager = managerEmployeeJsonFileManager;
        _regularEmployeeJsonFileManager = regularEmployeeJsonFileManager;
        _employeeCriteriaToFilterTypeMapper = employeeCriteriaToFilterTypeMapper;
    }

    public async Task<Result<IEnumerable<Employee>>> GetAllAsync(EmployeeCriteria criteria)
    {
        var employees = new List<Employee>();
        
        var managersResult = await _managerEmployeeJsonFileManager.ReadFromFileAsync();
        var fullTimeResult = await _regularEmployeeJsonFileManager.ReadFromFileAsync();
        
        if (managersResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Employee>>(managersResult.Errors);
        }
        
        if (fullTimeResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Employee>>(fullTimeResult.Errors);
        }
        
        employees.AddRange(managersResult.Value);
        employees.AddRange(fullTimeResult.Value);
        
        var filterType = _employeeCriteriaToFilterTypeMapper.Map(criteria).Value;
        return Result.Ok(filterType.Apply(employees.AsQueryable()));
    }
}