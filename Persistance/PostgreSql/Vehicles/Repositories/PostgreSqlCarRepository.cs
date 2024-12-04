using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.UseCases.Vehicles.Cars.Repositories;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Vehicles.Repositories;

public class PostgreSqlCarRepository : ICarRepository
{
    private readonly DatabaseConnectionFactory _connectionFactory;

    public PostgreSqlCarRepository(DatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    private const string InsertCommand =
        @"INSERT INTO ""Vehicle"" (""Id"", ""BrandName"", ""DateOfManufacture"",""LicensePlate"",""Color"",
            ""PricePerDay"",""EngineType"",""Seats"",""Model"",""VehicleType"",""EngineDisplacement"", 
            ""HelmetStorage"", ""TrunkSize"",""Doors"")
          VALUES (@Id, @BrandName, @DateOfManufacture, @LicensePlate, @Color, @PricePerDay, @EngineType, @Seats, 
            @Model, 0, NULL, NULL, @TrunkSize, @Doors)";

    public async Task<Result> InsertAsync(Car car)
    {
        car.Id = Guid.NewGuid();

        var parameters = new
        {
            car.Id,
            BrandName = car.Manufacturer.Name,
            car.DateOfManufacture,
            car.LicensePlate,
            car.Color,
            car.PricePerDay,
            car.EngineType,
            car.Seats,
            car.Model,
            TrunkSize = car.TrunkCapacity,
            car.Doors
        };

        await using var connection = await _connectionFactory.CreateConnection();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            await connection.ExecuteAsync(InsertCommand, parameters);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return Result.Fail("Failed to insert car");
        }

        await transaction.CommitAsync();
        
        return Result.Ok();
    }

}