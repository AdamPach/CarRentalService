using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Domain.Vehicles.ValueObjects;
using CarRentalService.Persistence.PostgreSql.Common;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.UseCases.Vehicles.Vehicles.Repository;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Vehicles.Repositories;

public class PostgreSqlVehicleRepository : IVehicleRepository
{
    private readonly DatabaseConnectionFactory  _connectionFactory;
    public readonly CriteriaToSqlMapper<VehicleCriteria> _criteriaToSqlMapper;

    public PostgreSqlVehicleRepository(
        DatabaseConnectionFactory connectionFactory,
        CriteriaToSqlMapper<VehicleCriteria> criteriaToSqlMapper)
    {
        _connectionFactory = connectionFactory;
        _criteriaToSqlMapper = criteriaToSqlMapper;
    }

    private const string QueryTemplate = 
        @"SELECT ""Id"", ""DateOfManufacture"", ""Model"", ""LicensePlate"", ""Color"", ""Seats"", ""EngineType"", ""PricePerDay"", ""VehicleType"", ""BrandName"" AS ""Name""
        FROM ""Vehicle"" /**innerjoin**//**where**/";

    public async Task<Result<IEnumerable<Vehicle>>> GetAllAsync(VehicleCriteria criteria)
    {
        var mapperResult = _criteriaToSqlMapper.Map(criteria);

        if (mapperResult.IsFailed)
        {
            return Result.Fail(mapperResult.Errors);
        }
        
        var sqlBuilder = mapperResult.Value;
        
        var query = sqlBuilder.AddTemplate(QueryTemplate);
        
        await using var connection = 
            await _connectionFactory.CreateConnection();

        var vehicles = await connection
            .QueryAsync<Vehicle, Manufacturer, Vehicle>(query.RawSql, param: query.Parameters, 
                map: (vehicle, manufacturer) =>
            {
                vehicle.Manufacturer = manufacturer;
                return vehicle;
            }, splitOn:"Name");
        
        return Result.Ok(vehicles);
    }
}