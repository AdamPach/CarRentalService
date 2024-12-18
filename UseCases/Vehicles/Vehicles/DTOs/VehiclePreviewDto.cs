﻿namespace CarRentalService.UseCases.Vehicles.Vehicles.DTOs;

public class VehiclePreviewDto
{
    public required Guid Id { get; set; }
    public required string Brand { get; set; }
    public required string LicensePlate { get; set; }
    public required string Model { get; set; }
    public required string VehicleType { get; set; }
    public decimal PricePerDay { get; set; }
    public required string EngineType { get; set; }
    public int Seats { get; set; }
    public required bool IsRented { get; set; }
}