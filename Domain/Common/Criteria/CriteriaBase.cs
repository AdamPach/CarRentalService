namespace CarRentalService.Domain.Common.Criteria;

public abstract record CriteriaBase
{
    public int RowSize { get; init; } = 0;
    public int RowIndex { get; init; } = 0;
}