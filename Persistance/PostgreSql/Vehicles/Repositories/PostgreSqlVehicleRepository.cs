using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Persistence.PostgreSql.Common;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.UseCases.Vehicles.Repository;
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

    private const string QueryTemplate = @"SELECT * FROM ""Vehicle"" /**innerjoin**//**where**/";

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

        var vehicles = await connection.QueryAsync<Vehicle>(query.RawSql, param: query.Parameters);
        
        return Result.Ok(vehicles);
    }

    public Task<Result<Vehicle>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}