using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.Domain.Rentals.ValueObjects;

namespace CarRentalService.UseCases.Rentals.DTOs;

public class RentalPreviewDto
{
    public required Guid Id { get; set; }
    
    public required RentalStatus Status { get; set; }
    
    public required RentalDateRange RentalDateRange { get; set; }
    
    public required string VehicleName { get; set; }
    
    public required string CustomerName { get; set; }
}