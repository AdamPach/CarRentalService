using CarRentalService.Domain.Rentals.Entities;

namespace CarRentalService.Domain.Persons.Entities;

public abstract class Employee : Person
{
    public string EmployeeNumber { get; set; } = string.Empty;
    
    public required DateTime HireDate { get; set; }
    
    public DateTime? TerminationDate { get; set; }
    
    public required decimal Salary { get; set; }
    
    public required string Password { get; set; }
    
    public ICollection<Rental>? Rentals { get; set; }
}