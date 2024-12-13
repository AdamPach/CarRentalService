using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Rentals.DTOs;
using FluentResults;

namespace CarRentalService.UseCases.Rentals.Updaters;

internal sealed class ReturnRentalUpdater : IUpdater<Rental, ReturnRentalDto>
{
    public Result<Rental> Update(Rental updated, ReturnRentalDto updater)
    {
        updated.RentalDateRange = updated.RentalDateRange.UpdateReturnDate(updater.ReturnDate!.Value);
        updated.Status = RentalStatus.Closed;
        updated.TotalPrice = updated.CalculateTotalPrice() + updater.AdditionalCosts;
        
        return updated;
    }
}