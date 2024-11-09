namespace CarRentalService.Domain.Entities.Vehicles;

public class Motorbike : Vehicle
{
    public required int EngineDisplacement { get; set; }
    
    public required bool HelmetStorage { get; set; }
}