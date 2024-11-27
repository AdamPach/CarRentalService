using CarRentalService.UseCases.Persons.Customers.DTOs;
using FluentValidation;

namespace CarRentalService.UseCases.Persons.Customers.Validators;

public class CreateCustomerDtoValidator : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .LessThan(DateTime.Now);
        
        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email));
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
        
        RuleFor(x => x.City)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.City));
        
        RuleFor(x => x.Street)
            .MaximumLength(50)
            .When(x => !string.IsNullOrEmpty(x.Street));
        
        RuleFor(x => x.ZipCode)
            .MaximumLength(10)
            .When(x => !string.IsNullOrEmpty(x.ZipCode));
        
        RuleFor(x => x.LicenseNumber)
            .NotEmpty()
            .MaximumLength(50);
        
    }
}