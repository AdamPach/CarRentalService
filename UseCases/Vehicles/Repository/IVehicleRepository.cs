using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Common;

namespace CarRentalService.UseCases.Vehicles.Repository;

public interface IVehicleRepository : IReadRepository<Vehicle, VehicleCriteria>;