using CarRentalService.Domain.Vehicles.Enums;
using CarRentalService.UseCases.Vehicles.Cars.DTOs;
using FluentValidation;

namespace CarRentalService.UseCases.Vehicles.Cars.Validators;

public sealed class CreateCarValidator : AbstractValidator<CreateCarDto>
{
    public CreateCarValidator()
    {
        RuleFor(x => x.Brand).NotEmpty();
        RuleFor(x => x.Model).NotEmpty();
        RuleFor(x => x.LicensePlate).NotEmpty();
        RuleFor(x => x.Seats).GreaterThan(0);
        RuleFor(x => x.EngineType).IsInEnum();
        RuleFor(x => x.PricePerDay).GreaterThan(0);
        RuleFor(x => x.TrunkCapacity).GreaterThan(0);
        RuleFor(x => x.Doors).GreaterThan(0);
    }
}