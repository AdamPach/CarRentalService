using CarRentalService.Domain.Common.Criteria;

namespace CarRentalService.Domain.Vehicles.Criteria;

public record VehicleCriteria : CriteriaBase
{
    public bool ExcludeRented { get; init; } = true;
}