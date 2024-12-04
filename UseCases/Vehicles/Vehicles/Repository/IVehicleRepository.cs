using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles.Vehicles.Repository;

public interface IVehicleRepository
{
    Task<Result<IEnumerable<Vehicle>>> GetAllAsync(VehicleCriteria criteria);
}
