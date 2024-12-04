using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Vehicles.Cars.DTOs;
using CarRentalService.UseCases.Vehicles.Cars.Repositories;
using FluentResults;
using FluentValidation;

namespace CarRentalService.UseCases.Vehicles.Cars;

internal sealed class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IValidator<CreateCarDto> _createCarValidator;
    private readonly IMapper<CreateCarDto, Car> _createCarToEntityMapper;

    public CarService(
        ICarRepository carRepository,
        IValidator<CreateCarDto> createCarValidator,
        IMapper<CreateCarDto, Car> createCarToEntityMapper)
    {
        _carRepository = carRepository;
        _createCarValidator = createCarValidator;
        _createCarToEntityMapper = createCarToEntityMapper;
    }

    public async Task<Result> CreateCarAsync(CreateCarDto addCarDto)
    {
        var validationResult = await _createCarValidator.ValidateAsync(addCarDto);
        
        if (!validationResult.IsValid)
        {
            return Result.Fail(validationResult.Errors.Select(error => error.ErrorMessage));
        }
        
        var carResult = _createCarToEntityMapper.Map(addCarDto);
        
        if (carResult.IsFailed)
        {
            return Result.Fail(carResult.Errors);
        }
        
        var result = await _carRepository.InsertAsync(carResult.Value);
        
        return result.IsSuccess ? Result.Ok() : Result.Fail(result.Errors);
    }
}