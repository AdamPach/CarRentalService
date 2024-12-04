using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Domain.Vehicles.Enums;
using CarRentalService.Domain.Vehicles.ValueObjects;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Vehicles.Cars.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles.Cars.Mappers;

internal sealed class CreateCarToEntityMapper : IMapper<CreateCarDto, Car>
{
    public Result<Car> Map(CreateCarDto from)
    {
        var car = new Car
        {
            Manufacturer = new Manufacturer(from.Brand),
            Model = from.Model,
            PricePerDay = from.PricePerDay,
            EngineType = from.EngineType,
            Seats = from.Seats,
            DateOfManufacture = from.ProductionDate!.Value,
            Doors = from.Doors,
            LicensePlate = from.LicensePlate,
            TrunkCapacity = from.TrunkCapacity,
            Color = string.IsNullOrWhiteSpace(from.Color) ? null : from.Color,
            VehicleType = VehicleType.Car
        };

        return car;
    }
}