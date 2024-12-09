using CarRentalService.Domain.Common.Criteria;

namespace CarRentalService.Domain.Vehicles.Criteria;

public record VehicleCriteria : CriteriaBase
{
    public string Brand { get; init; } = string.Empty;
    public bool ExcludeRented { get; init; } = true;
}