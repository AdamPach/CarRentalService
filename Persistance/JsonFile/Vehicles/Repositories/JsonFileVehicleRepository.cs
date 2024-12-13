using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Persistence.JsonFile.FileManager;
using CarRentalService.Persistence.JsonFile.Vehicles.Mappers;
using CarRentalService.UseCases.Vehicles.Vehicles.Repository;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Vehicles.Repositories;

internal sealed class JsonFileVehicleRepository : IVehicleRepository
{
    private readonly JsonFileManager<Car> _carFileManager;
    private readonly JsonFileManager<Motorbike> _motorbikeFileManager;
    private readonly JsonFileManager<Rental> _rentalFileManager;
    
    private readonly VehicleCriteriaToFilterTypeMapper _vehicleCriteriaToFilterTypeMapper;

    public JsonFileVehicleRepository(
        JsonFileManager<Car> carFileManager,
        JsonFileManager<Motorbike> motorbikeFileManager, 
        VehicleCriteriaToFilterTypeMapper vehicleCriteriaToFilterTypeMapper,
        JsonFileManager<Rental> rentalFileManager)
    {
        _carFileManager = carFileManager;
        _motorbikeFileManager = motorbikeFileManager;
        _vehicleCriteriaToFilterTypeMapper = vehicleCriteriaToFilterTypeMapper;
        _rentalFileManager = rentalFileManager;
    }

    public async Task<Result<IEnumerable<Vehicle>>> GetAllAsync(VehicleCriteria criteria)
    {
        var vehicles = new List<Vehicle>();

        var carsResult = await _carFileManager.ReadFromFileAsync();
        var motorbikesResult = await _motorbikeFileManager.ReadFromFileAsync();
        
        if (carsResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Vehicle>>(carsResult.Errors);
        }
        
        if (motorbikesResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Vehicle>>(motorbikesResult.Errors);
        }
        
        vehicles.AddRange(carsResult.Value);
        vehicles.AddRange(motorbikesResult.Value);
        
        var rentalsResult = await _rentalFileManager.ReadFromFileAsync();
        
        if (rentalsResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Vehicle>>(rentalsResult.Errors);
        }
        
        var rentals = rentalsResult.Value;

        foreach (var rental in rentals)
        {
            var vehicle = vehicles.Single(v => v.Id == rental.VehicleId);
            vehicle.Rentals ??= new List<Rental>();
            vehicle.Rentals.Add(rental);
        }

        var filterType = _vehicleCriteriaToFilterTypeMapper.Map(criteria).Value;
        return Result.Ok(filterType.Apply(vehicles.AsQueryable()));
    }

    public async Task<Result<Vehicle>> GetByIdAsync(Guid vehicleId)
    {
        var vehiclesResult = await GetAllAsync(new VehicleCriteria());
        
        if (vehiclesResult.IsFailed)
        {
            return Result.Fail<Vehicle>(vehiclesResult.Errors);
        }

        var vehicles = vehiclesResult.Value
            .Where( e => e.Id == vehicleId)
            .ToList();
        
        return vehicles.Count switch
        {
            0 => Result.Fail<Vehicle>("Vehicle not found"),
            1 => Result.Ok(vehicles.Single(v => v.Id == vehicleId)),
            _ => Result.Fail<Vehicle>("Multiple vehicles found")
        };
    }
}