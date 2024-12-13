using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Persistence.JsonFile.Common;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.People.Mappers;

internal sealed class CustomerCriteriaToFilterTypeMapper : CriteriaToLinqMapper<Customer, CustomerCriteria>
{
    public override Result<LinqFilterType<Customer>> Map(CustomerCriteria from)
    {
        var filter =  base.Map(from).Value;

        if (!string.IsNullOrWhiteSpace(from.FirstName))
        {
            filter.AddWhereExpression(e => e.FirstName.Contains(from.FirstName));
        }

        return filter;
    }
}