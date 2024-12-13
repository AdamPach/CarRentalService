using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Persistence.JsonFile.Common;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.People.Mappers;

public class EmployeeCriteriaToFilterTypeMapper : CriteriaToLinqMapper<Employee, EmployeeCriteria>
{
    public override Result<LinqFilterType<Employee>> Map(EmployeeCriteria from)
    {
        var filter = base.Map(from).Value;
        
        if (!string.IsNullOrWhiteSpace(from.EmployeeNumber))
        {
            filter.AddWhereExpression(e => e.EmployeeNumber == from.EmployeeNumber);
        }
        
        return filter;
    }
}