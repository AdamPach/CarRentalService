using CarRentalService.Domain.Common.Criteria;

namespace CarRentalService.Domain.Persons.Criteria;

public record EmployeeCriteria : CriteriaBase
{
    public string EmployeeNumber { get; init; } = string.Empty;

    public static EmployeeCriteria Empty => new();
}