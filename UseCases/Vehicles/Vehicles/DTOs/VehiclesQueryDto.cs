using CarRentalService.Domain.Vehicles.Criteria;

namespace CarRentalService.UseCases.Vehicles.Vehicles.DTOs;

public record VehiclesQueryDto
{
    public string Brand { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    
    public VehicleCriteria ToCriteria()
    {
        return new VehicleCriteria
        {
            Brand = Brand,
            ExcludeRented = IsAvailable
        };
    }
    
    public static implicit operator VehicleCriteria(VehiclesQueryDto dto)
    {
        return dto.ToCriteria();
    }
}