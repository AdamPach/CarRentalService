using CarRentalService.Domain.Vehicles.Entities;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles.Cars.Repositories;

public interface ICarRepository
{
    Task<Result> InsertAsync(Car car);
}