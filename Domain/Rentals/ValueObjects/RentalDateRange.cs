namespace CarRentalService.Domain.Rentals.ValueObjects;

public record RentalDateRange(DateTime StartDate, DateTime EndDate, DateTime? ReturnDate)
{
    public bool IsDateRangeValid()
    {
        return StartDate < EndDate && (ReturnDate == null || StartDate < ReturnDate);
    }
}