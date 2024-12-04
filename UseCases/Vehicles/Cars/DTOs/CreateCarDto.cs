using CarRentalService.Domain.Vehicles.Enums;

namespace CarRentalService.UseCases.Vehicles.Cars.DTOs;

public class CreateCarDto
{
    public string Brand { get; set; } = string.Empty;
    public DateTime? ProductionDate { get; set; }
    public string Model { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public string? Color { get; set; }
    public int Seats { get; set; }
    public EngineType EngineType { get; set; }
    public decimal PricePerDay { get; set; }
    public int TrunkCapacity { get; set; }
    public int Doors { get; set; }
}