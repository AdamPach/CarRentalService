using CarRentalService.UseCases.Vehicles.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles;

public interface IVehicleService
{
    Task<Result<IEnumerable<VehiclePreviewDto>>> GetAllVehiclesPreviewsAsync();
}