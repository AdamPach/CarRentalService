using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Vehicles.Vehicles.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles.Vehicles.Mappers;

internal sealed class VehicleEntityToPreviewDtoMapper 
    : IMapper<Vehicle, VehiclePreviewDto>
{
    public Result<VehiclePreviewDto> Map(Vehicle from)
    {
        var vehiclePreview = new VehiclePreviewDto
        {
            Id = from.Id,
            Brand = from.Manufacturer.Name,
            Model = from.Model,
            VehicleType = from.VehicleType.ToString(),
            PricePerDay = from.PricePerDay,
            EngineType = from.EngineType.ToString(),
            Seats = from.Seats
        };

        return vehiclePreview;
    }
}