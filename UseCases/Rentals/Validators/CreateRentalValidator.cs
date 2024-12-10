using CarRentalService.UseCases.Rentals.DTOs;
using FluentValidation;

namespace CarRentalService.UseCases.Rentals.Validators;

public class CreateRentalValidator : AbstractValidator<CreateRentalDto>
{
    public CreateRentalValidator()
    {
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .LessThan(x => x.EndDate)
            .WithMessage("Start date must be before end date");
        
        RuleFor(x => x.EndDate).NotEmpty();
    }
}