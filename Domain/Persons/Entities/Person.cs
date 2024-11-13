using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.Domain.Persons.ValueObjects;

namespace CarRentalService.Domain.Persons.Entities;

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