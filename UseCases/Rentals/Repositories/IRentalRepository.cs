using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using FluentResults;

namespace CarRentalService.UseCases.Rentals.Repositories;

public interface IRentalRepository
{
    Task<Result<IEnumerable<Rental>>> GetAllWithCustomerAndVehicleAsync(RentalCriteria criteria);
}