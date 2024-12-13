namespace CarRentalService.UseCases.Rentals.DTOs;

public class ReturnRentalDto
{
    public Guid RentalId { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal AdditionalCosts { get; set; }
}