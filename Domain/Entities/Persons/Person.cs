using CarRentalService.Domain.Entities.Common.Interfaces;

namespace CarRentalService.Domain.Entities.Persons;

public abstract class Person : IEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
    public required DateTime DateOfBirth { get; set; }
    
    public string? Email { get; set; }
    
    public required string PhoneNumber { get; set; }
    
    public Address? Address { get; set; }
}