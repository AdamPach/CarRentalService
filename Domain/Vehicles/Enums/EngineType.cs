namespace CarRentalService.Domain.Vehicles.Enums;

public enum EngineType
{
    Gasoline = 0,
    Diesel,
    Electric,
    Hybrid,
}

public static class EngineTypeExtension
{
    public static EngineType ToEngineType(this int value)
    {
        return value switch
        {
            0 => EngineType.Gasoline,
            1 => EngineType.Diesel,
            2 => EngineType.Electric,
            3 => EngineType.Hybrid,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Invalid value for EngineType")
        };
    }
} 