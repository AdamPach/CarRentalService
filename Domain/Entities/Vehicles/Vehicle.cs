using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.Domain.Entities.Rentals;

namespace CarRentalService.Domain.Entities.Vehicles;

public abstract class Vehicle : IEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    
    public required Manufacturer Manufacturer { get; set; } 
    
    public required DateTime DateOfManufacture { get; set; }
    
    public required Model Model { get; set; } 
    
    public required string LicensePlate { get; set; }
    
    public string? Color { get; set; }
    
    public required int Seats { get; set; }
    
    public required EngineType EngineType { get; set; }
    
    public required decimal PricePerDay { get; set; }
    
    public ICollection<Rental>? Rentals { get; set; }
}