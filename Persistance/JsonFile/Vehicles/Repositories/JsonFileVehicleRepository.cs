using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Vehicles.Vehicles.Repository;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Vehicles.Repositories;

public class JsonFileVehicleRepository : IVehicleRepository
{
    public Task<Result<IEnumerable<Vehicle>>> GetAllAsync(VehicleCriteria criteria)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Vehicle>> GetByIdAsync(Guid vehicleId)
    {
        throw new NotImplementedException();
    }
}