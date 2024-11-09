using CarRentalService.Domain.Entities.Rentals;

namespace CarRentalService.Domain.Entities.Persons;

public class Customer : Person
{
    public required DateTime RegistrationDate { get; set; }
    
    public required string LicenseNumber { get; set; }
    
    public ICollection<Rental>? Rentals { get; set; }
}