using CarRentalService.Domain.Common.Criteria;

namespace CarRentalService.Domain.Persons.Criteria;

public record CustomerCriteria : CriteriaBase
{
    public string FirstName { get; init; } = string.Empty;
    public static CustomerCriteria Empty => new();
}