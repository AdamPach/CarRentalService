using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.UseCases.Rentals.Repositories;
using FluentResults;

namespace CarRentalService.UseCases.Rentals;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;

    public RentalService(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public async Task<Result<IEnumerable<Rental>>> GetActiveRentalsAsync()
    {
        var criteria = new RentalCriteria
        {
            Status = RentalStatus.Active
        };

        var rentals = await _rentalRepository.GetAllAsync(criteria);
        
        return rentals;
    }
}