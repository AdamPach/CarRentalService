using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.UseCases.Rentals.Repositories;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Rentals.Repositories;

public class JsonFileRentalRepository : IRentalRepository
{
    public Task<Result<IEnumerable<Rental>>> GetAllWithCustomerAndVehicleAsync(RentalCriteria criteria)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Rental>> GetByIdWithCustomerAndVehicleAsync(Guid rentalId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Rental>> CreateRentalAsync(Rental rental)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateRentalAsync(Rental rental)
    {
        throw new NotImplementedException();
    }
}