using CarRentalService.Domain.Entities.Rentals;

namespace CarRentalService.Domain.Entities.Persons;

public class Employee : Person
{
    public string EmployeeNumber { get; set; } = string.Empty;
    
    public required DateTime HireDate { get; set; }
    
    public DateTime? TerminationDate { get; set; }
    
    public required decimal Salary { get; set; }
    
    public required EmployeeType EmployeeType { get; set; }

    public required string Password { get; set; }
    
    public ICollection<Rental>? Rentals { get; set; }
}