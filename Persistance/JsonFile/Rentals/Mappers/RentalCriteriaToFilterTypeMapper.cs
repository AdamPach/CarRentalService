using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Persistence.JsonFile.Common;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Rentals.Mappers;

public class RentalCriteriaToFilterTypeMapper : CriteriaToLinqMapper<Rental, RentalCriteria>
{
    public override Result<LinqFilterType<Rental>> Map(RentalCriteria from)
    {
        var filter = base.Map(from).Value;
        
        if(from.Status.HasValue)
        {
            filter.AddWhereExpression(e => e.Status == from.Status);
        }
        
        if(from.RentalId != Guid.Empty)
        {
            filter.AddWhereExpression(e => e.Id == from.RentalId);
        }
        
        return filter;
    }
}