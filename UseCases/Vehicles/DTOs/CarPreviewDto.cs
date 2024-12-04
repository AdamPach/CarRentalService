namespace CarRentalService.UseCases.Vehicles.DTOs;

public class VehiclePreviewDto
{
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public required string VehicleType { get; set; }
    public decimal PricePerDay { get; set; }
    public required string EngineType { get; set; }
    public int Seats { get; set; }
}