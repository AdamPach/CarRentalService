using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.Domain.Rentals.ValueObjects;
using CarRentalService.Domain.Vehicles.Entities;

namespace CarRentalService.Domain.Rentals.Entities;

public class Rental : IEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    
    public required RentalStatus Status { get; set; }
    
    public required RentalDateRange RentalDateRange { get; set; }
    
    public required Guid VehicleId { get; set; }
    
    public Vehicle? Vehicle { get; set; }
    
    public required Guid CustomerId { get; set; }
    
    public Customer? Customer { get; set; }
    
    public required Guid EmployeeId { get; set; }
    
    public Employee? Employee { get; set; }
}