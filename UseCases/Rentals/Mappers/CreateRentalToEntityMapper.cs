using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.Domain.Rentals.ValueObjects;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Rentals.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Rentals.Mappers;

public class CreateRentalToEntityMapper : IMapper<CreateRentalDto, Rental>
{
    public Result<Rental> Map(CreateRentalDto from)
    {
        var dateRange = new RentalDateRange(from.StartDate!.Value, from.EndDate!.Value);
        
        var rental = new Rental
        {
            CustomerId = from.CustomerId,
            VehicleId = from.VehicleId,
            EmployeeId = from.EmployeeId,
            RentalDateRange = dateRange,
            Status = RentalStatus.Active
        };

        return rental;
    }
}