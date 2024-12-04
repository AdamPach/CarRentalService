using CarRentalService.UseCases.Vehicles.Cars.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Vehicles.Cars;

public interface ICarService
{
    Task<Result> CreateCarAsync(CreateCarDto addCarDto);
}