using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Rentals.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Rentals.Mappers;

public class RentalToDetailDtoMapper : IMapper<Rental, RentalDetailDto>
{
    public Result<RentalDetailDto> Map(Rental from) =>
        new RentalDetailDto
        {
            Id = from.Id,
            CustomerName = from.Customer!.FullName,
            VehicleName = from.Vehicle!.Name,
            RentalDateRange = from.RentalDateRange,
            Status = from.Status,
            TotalPrice = from.TotalPrice
        };
}