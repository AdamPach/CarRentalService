using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Rentals.DTOs;
using CarRentalService.UseCases.Rentals.Repositories;
using FluentResults;
using FluentValidation;

namespace CarRentalService.UseCases.Rentals;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IMapper<Rental, RentalPreviewDto> _rentalToPreviewDtoMapper;
    
    private readonly IValidator<CreateRentalDto> _createRentalDtoValidator;
    private readonly IMapper<CreateRentalDto, Rental> _createRentalDtoToRentalMapper;

    public RentalService(
        IRentalRepository rentalRepository, 
        IMapper<Rental, RentalPreviewDto> rentalToPreviewDtoMapper, 
        IValidator<CreateRentalDto> createRentalDtoValidator, 
        IMapper<CreateRentalDto, Rental> createRentalDtoToRentalMapper)
    {
        _rentalRepository = rentalRepository;
        _rentalToPreviewDtoMapper = rentalToPreviewDtoMapper;
        _createRentalDtoValidator = createRentalDtoValidator;
        _createRentalDtoToRentalMapper = createRentalDtoToRentalMapper;
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
}