using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Rentals.DTOs;
using CarRentalService.UseCases.Rentals.Repositories;
using CarRentalService.UseCases.Rentals.Updaters;
using FluentResults;
using FluentValidation;

namespace CarRentalService.UseCases.Rentals;

internal class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IMapper<Rental, RentalPreviewDto> _rentalToPreviewDtoMapper;
    private readonly IMapper<Rental, RentalDetailDto> _rentalToDetailDtoMapper;
    private readonly ReturnRentalUpdater _returnRentalUpdater;
    private readonly IValidator<(ReturnRentalDto, Rental)> _returnRentalValidator;
    private readonly IValidator<CreateRentalDto> _createRentalDtoValidator;
    private readonly IMapper<CreateRentalDto, Rental> _createRentalDtoToRentalMapper;

    public RentalService(
        IRentalRepository rentalRepository, 
        IMapper<Rental, RentalPreviewDto> rentalToPreviewDtoMapper, 
        IValidator<CreateRentalDto> createRentalDtoValidator, 
        IMapper<CreateRentalDto, Rental> createRentalDtoToRentalMapper, 
        IMapper<Rental, RentalDetailDto> rentalToDetailDtoMapper, 
        ReturnRentalUpdater returnRentalUpdater, 
        IValidator<(ReturnRentalDto, Rental)> returnRentalValidator)
    {
        _rentalRepository = rentalRepository;
        _rentalToPreviewDtoMapper = rentalToPreviewDtoMapper;
        _createRentalDtoValidator = createRentalDtoValidator;
        _createRentalDtoToRentalMapper = createRentalDtoToRentalMapper;
        _rentalToDetailDtoMapper = rentalToDetailDtoMapper;
        _returnRentalUpdater = returnRentalUpdater;
        _returnRentalValidator = returnRentalValidator;
    }

    public async Task<Result<IEnumerable<RentalPreviewDto>>> GetActiveRentalsAsync()
    {
        var criteria = new RentalCriteria
        {
            Status = RentalStatus.Active,
        };

        var rentals = await _rentalRepository
            .GetAllWithCustomerAndVehicleAsync(criteria);
        
        if (rentals.IsFailed)
        {
            return Result.Fail("Failed to get active rentals");
        }
        
        var rentalDtos = rentals.Value
            .Select(rental => _rentalToPreviewDtoMapper.Map(rental).Value);
        
        return Result.Ok(rentalDtos);
    }

    public async Task<Result> CreateRentalAsync(CreateRentalDto createRentalDto)
    {
        var validationResult = await _createRentalDtoValidator.ValidateAsync(createRentalDto);
        
        if (!validationResult.IsValid)
        {
            return Result.Fail("Invalid new Rental!");
        }
        
        var rental = _createRentalDtoToRentalMapper.Map(createRentalDto).Value;
        
        var result = await _rentalRepository.CreateRentalAsync(rental);
        
        if (result.IsFailed)
        {
            return Result.Fail("Failed to create rental");
        }
        
        return Result.Ok();
    }

    public async Task<Result<RentalDetailDto>> GetRentalDetailAsync(Guid rentalId)
    {
        var result = await _rentalRepository.GetByIdWithCustomerAndVehicleAsync(rentalId);
        
        if (result.IsFailed)
        {
            return Result.Fail<RentalDetailDto>("Failed to get rental detail");
        }
        
        var rental = result.Value;
        
        return _rentalToDetailDtoMapper.Map(rental).Value;
    }
    
    public async Task<Result> ReturnRentalAsync(ReturnRentalDto returnRental)
    {
        var result = await _rentalRepository.GetByIdWithCustomerAndVehicleAsync(returnRental.RentalId);
        
        if (result.IsFailed)
        {
            return Result.Fail("Failed to get rental detail");
        }
        
        var validationResult = await _returnRentalValidator.ValidateAsync((returnRental, result.Value));
        
        if (!validationResult.IsValid)
        {
            return Result.Fail("Invalid return rental!");
        }
        
        var updateResult = _returnRentalUpdater.Update(result.Value, returnRental);
        
        if (updateResult.IsFailed)
        {
            return Result.Fail("Failed to update rental");
        }
        
        var dataResult = await _rentalRepository.UpdateRentalAsync(updateResult.Value);
        
        if (dataResult.IsFailed)
        {
            return Result.Fail("Failed to update rental");
        }
        
        return Result.Ok();
    }
    
}