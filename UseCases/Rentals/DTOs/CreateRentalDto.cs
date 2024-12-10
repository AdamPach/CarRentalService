namespace CarRentalService.UseCases.Rentals.DTOs;

public class CreateRentalDto
{
    public Guid VehicleId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}