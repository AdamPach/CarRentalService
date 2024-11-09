namespace CarRentalService.Domain.Entities.Rentals;

public record RentalDateRange(DateTime StartDate, DateTime EndDate)
{
    public bool IsDateRangeValid()
    {
        return StartDate < EndDate;
    }
}