using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Vehicles.Cars.Repositories;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Vehicles.Repositories;

public class JsonFileCarRepository : ICarRepository
{
    public Task<Result> InsertAsync(Car car)
    {
        throw new NotImplementedException();
    }
}