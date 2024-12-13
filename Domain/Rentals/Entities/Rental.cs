using System.ComponentModel.DataAnnotations.Schema;
using CarRentalService.Domain.Common.Interfaces;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.Domain.Rentals.ValueObjects;
using CarRentalService.Domain.Vehicles.Entities;

namespace CarRentalService.Domain.Rentals.Entities;

[Table("Rentals")]
public class Rental : IEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    
    public required RentalStatus Status { get; set; }
    
    public required RentalDateRange RentalDateRange { get; set; }
    
    public decimal TotalPrice { get; set; }
    
    public required Guid VehicleId { get; set; }
    
    public Vehicle? Vehicle { get; set; }
    
    public required Guid CustomerId { get; set; }
    
    public Customer? Customer { get; set; }
    
    public required Guid EmployeeId { get; set; }
    
    public Employee? Employee { get; set; }
    
    public decimal CalculateTotalPrice()
    {
        return (RentalDateRange.ReturnDate! - RentalDateRange.StartDate).Value.Days * Vehicle!.PricePerDay;
    }
}