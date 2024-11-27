namespace CarRentalService.UseCases.Persons.Customers.DTOs;

public class CreateCustomerDto
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public DateTime? DateOfBirth { get; set; }
    
    public string? Email { get; set; }
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public string City { get; set; } = string.Empty;
    
    public string Street { get; set; } = "VSB";
    
    public string ZipCode { get; set; } = "777777";
    
    public string LicenseNumber { get; set; } = string.Empty;
}