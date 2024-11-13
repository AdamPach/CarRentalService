namespace CarRentalService.Domain.Rentals.ValueObjects;

public record RentalDateRange(DateTime StartDate, DateTime EndDate)
{
    public bool IsDateRangeValid()
    {
        return StartDate < EndDate;
    }
}