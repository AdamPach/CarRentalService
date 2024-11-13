using CarRentalService.Domain.Rentals.Entities;

namespace CarRentalService.Domain.Persons.Entities;

public class Customer : Person
{
    public required DateTime RegistrationDate { get; set; }
    
    public required string LicenseNumber { get; set; }
    
    public ICollection<Rental>? Rentals { get; set; }
}