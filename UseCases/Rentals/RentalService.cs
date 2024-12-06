using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Rentals.DTOs;
using CarRentalService.UseCases.Rentals.Repositories;
using FluentResults;

namespace CarRentalService.UseCases.Rentals;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IMapper<Rental, RentalPreviewDto> _rentalToPreviewDtoMapper;

    public RentalService(
        IRentalRepository rentalRepository, 
        IMapper<Rental, RentalPreviewDto> rentalToPreviewDtoMapper)
    {
        _rentalRepository = rentalRepository;
        _rentalToPreviewDtoMapper = rentalToPreviewDtoMapper;
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
}