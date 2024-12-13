using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Persistence.JsonFile.FileManager;
using CarRentalService.UseCases.Vehicles.Cars.Repositories;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Vehicles.Repositories;

internal sealed class JsonFileCarRepository : ICarRepository
{
    private readonly JsonFileManager<Car> _carFileManager;

    public JsonFileCarRepository(JsonFileManager<Car> carFileManager)
    {
        _carFileManager = carFileManager;
    }

    public async Task<Result> InsertAsync(Car car)
    {
        var result = await _carFileManager.ReadFromFileAsync();
        
        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }
        
        var cars = result.Value.ToList();
        
        cars.Add(car);
        
        return await _carFileManager.WriteToFileAsync(cars);
    }
}