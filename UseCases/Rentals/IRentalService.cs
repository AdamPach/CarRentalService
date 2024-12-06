using CarRentalService.UseCases.Rentals.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Rentals;

public interface IRentalService
{
    Task<Result<IEnumerable<RentalPreviewDto>>> GetActiveRentalsAsync();
}