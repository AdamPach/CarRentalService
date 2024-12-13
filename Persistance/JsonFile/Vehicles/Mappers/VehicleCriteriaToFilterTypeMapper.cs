using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Persistence.JsonFile.Common;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Vehicles.Mappers;

public class VehicleCriteriaToFilterTypeMapper : CriteriaToLinqMapper<Vehicle, VehicleCriteria>
{
    public override Result<LinqFilterType<Vehicle>> Map(VehicleCriteria from)
    {
        var filter = base.Map(from).Value;

        if (from.ExcludeRented)
        {
            filter.AddWhereExpression( e => e.Rentals!.All( r => r.Status != RentalStatus.Active));
        }
        
        if(!string.IsNullOrWhiteSpace(from.Brand))
        {
            filter.AddWhereExpression(e => e.Manufacturer.Name == from.Brand);
        }

        return filter;
    }
}