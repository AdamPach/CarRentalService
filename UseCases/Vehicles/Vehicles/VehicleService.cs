using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Vehicles.Vehicles.DTOs;
using CarRentalService.UseCases.Vehicles.Vehicles.Repository;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles.Vehicles;

internal sealed class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    private readonly IMapper<Vehicle, VehiclePreviewDto> _previewMapper;

    public VehicleService(
        IVehicleRepository vehicleRepository,
        IMapper<Vehicle, VehiclePreviewDto> previewMapper)
    {
        _vehicleRepository = vehicleRepository;
        _previewMapper = previewMapper;
    }

    public async Task<Result<IEnumerable<VehiclePreviewDto>>> GetVehiclesPreviewsAsync(VehiclesQueryDto queryDto)
    {
        var vehicles = await _vehicleRepository.GetAllAsync(queryDto);
        
        if (vehicles.IsFailed)
        {
            return Result.Fail<IEnumerable<VehiclePreviewDto>>(vehicles.Errors);
        }
        
        var vehiclePreviews = vehicles.Value
            .Select(vehicle => _previewMapper.Map(vehicle).Value);
        
        return Result.Ok(vehiclePreviews);
    }
}