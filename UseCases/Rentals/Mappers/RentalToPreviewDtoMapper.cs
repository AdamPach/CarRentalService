using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Rentals.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Rentals.Mappers;

public class RentalToPreviewDtoMapper : IMapper<Rental, RentalPreviewDto>
{
    public Result<RentalPreviewDto> Map(Rental from)
    {
        var dto = new RentalPreviewDto
        {
            Id = from.Id,
            Status = from.Status,
            RentalDateRange = from.RentalDateRange,
            VehicleName = from.Vehicle?.Name 
                          ?? throw new ArgumentNullException("Rental.Vehicle"),
            CustomerName = from.Customer?.FullName 
                           ?? throw new ArgumentNullException("Rental.Customer"),
        };

        return Result.Ok(dto);
    }
}