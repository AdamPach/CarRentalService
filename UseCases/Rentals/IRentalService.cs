using CarRentalService.UseCases.Rentals.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Rentals;

public interface IRentalService
{
    Task<Result<IEnumerable<RentalPreviewDto>>> GetActiveRentalsAsync();
    Task<Result<RentalDetailDto>> GetRentalDetailAsync(Guid rentalId);
    Task<Result> CreateRentalAsync(CreateRentalDto createRentalDto);
    Task<Result> ReturnRentalAsync(ReturnRentalDto returnRental);
}