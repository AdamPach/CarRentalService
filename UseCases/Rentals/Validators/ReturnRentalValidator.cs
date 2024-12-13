using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.UseCases.Rentals.DTOs;
using FluentValidation;

namespace CarRentalService.UseCases.Rentals.Validators;

public class ReturnRentalValidator : AbstractValidator<(ReturnRentalDto, Rental)>
{
    public ReturnRentalValidator()
    {
        RuleFor( x => x.Item1.ReturnDate)
            .NotNull()
            .GreaterThan( x => x.Item2.RentalDateRange.StartDate)
            .WithMessage("Return date must be after rental start date");

        RuleFor(x => x.Item2.Status)
            .Must(s => s == RentalStatus.Active)
            .WithMessage("Rental must be active to be returned");
    }
}