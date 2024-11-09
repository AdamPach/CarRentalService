using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.Domain.Entities.Persons;
using CarRentalService.Domain.Entities.Vehicles;

namespace CarRentalService.Domain.Entities.Rentals;

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