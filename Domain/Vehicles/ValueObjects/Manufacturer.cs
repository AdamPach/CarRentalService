namespace CarRentalService.Domain.Vehicles.ValueObjects;

public record Manufacturer(string Name)
{
    public static Manufacturer Empty => new (string.Empty);
}