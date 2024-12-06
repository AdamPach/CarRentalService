using CarRentalService.Domain.Common.Criteria;
using CarRentalService.Domain.Rentals.Enums;

namespace CarRentalService.Domain.Rentals.Criteria;

public record RentalCriteria : CriteriaBase
{
    public bool IncludeVehicle { get; init; } = false;
    public bool IncludeCustomer { get; init; } = false;
    public bool IncludeEmployee { get; init; } = false;
    
    public RentalStatus? Status { get; init; } = null;
}