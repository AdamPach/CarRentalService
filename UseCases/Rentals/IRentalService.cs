using CarRentalService.Domain.Rentals.Entities;
using FluentResults;

namespace CarRentalService.UseCases.Rentals;

public interface IRentalService
{
    Task<Result<IEnumerable<Rental>>> GetActiveRentalsAsync();
}