namespace CarRentalService.UseCases.Persons.Customers.DTOs;

public class CustomerDetailDto
{
    public required Guid Id { get; set; }
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required DateTime DateOfBirth { get; set; }
    
    public string? Email { get; set; }
    
    public required string PhoneNumber { get; set; }
    
    public string? City { get; set; }
    
    public string? Street { get; set; }
    
    public string? ZipCode { get; set; }
    
    public required string LicenseNumber { get; set; }
    
    public required DateTime RegistrationDate { get; set; }
}