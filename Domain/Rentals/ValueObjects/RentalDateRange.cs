namespace CarRentalService.Domain.Rentals.ValueObjects;

public record RentalDateRange(DateTime StartDate, DateTime EndDate, DateTime? ReturnDate = null)
{
    public bool IsDateRangeValid()
    {
        return StartDate < EndDate && (ReturnDate == null || StartDate < ReturnDate);
    }
    
    public RentalDateRange UpdateReturnDate(DateTime returnDate)
    {
        return this with { ReturnDate = returnDate };
    }
}