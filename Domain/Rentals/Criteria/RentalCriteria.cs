using CarRentalService.Domain.Common.Criteria;
using CarRentalService.Domain.Rentals.Enums;

namespace CarRentalService.Domain.Rentals.Criteria;

public record RentalCriteria : CriteriaBase
{
    public RentalStatus? Status { get; init; } = null;
}