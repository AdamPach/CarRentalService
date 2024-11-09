namespace CarRentalService.Domain.Entities.Vehicles;

public class Car : Vehicle
{
    public required int Doors { get; set; }
    
    public required int TrunkCapacity { get; set; }
}