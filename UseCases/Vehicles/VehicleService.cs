using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Vehicles.DTOs;
using CarRentalService.UseCases.Vehicles.Repository;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles;

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

    public async Task<Result<IEnumerable<VehiclePreviewDto>>> GetAllVehiclesPreviewsAsync()
    {
        var criteria = new VehicleCriteria();
        
        var vehicles = await _vehicleRepository.GetAllAsync(criteria);
        
        if (vehicles.IsFailed)
        {
            return Result.Fail<IEnumerable<VehiclePreviewDto>>(vehicles.Errors);
        }
        
        var vehiclePreviews = vehicles.Value
            .Select(vehicle => _previewMapper.Map(vehicle).Value);
        
        return Result.Ok(vehiclePreviews);
    }
}