using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.ValueObjects;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.UseCases.Rentals.Repositories;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Rentals.Repositories;

public class PostgreSqlRentalRepository : IRentalRepository
{
    private readonly DatabaseConnectionFactory _connectionFactory;

    public PostgreSqlRentalRepository(DatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    private const string QueryTemplate = 
            @"SELECT /**select**/FROM ""Rentals"" /**innerjoin**//**where**/";

    public async Task<Result<IEnumerable<Rental>>> GetAllAsync(RentalCriteria criteria)
    {
        var sqlBuilder = new SqlBuilder();

        sqlBuilder.Select(@"""Id"", ""Status""");
        sqlBuilder.Select(@"""StartDate"", ""EndDate"",  ""ReturnDate""");
        
        var query = sqlBuilder.AddTemplate(QueryTemplate);
        
        await using var connection = await _connectionFactory.CreateConnection();
        
        var rentals = await connection
            .QueryAsync<Rental, RentalDateRange, Rental>(query.RawSql, param:query.Parameters,
                map:(rental, rentalDateRange) =>
                {
                    rental.RentalDateRange = rentalDateRange;
                    return rental;
                }, splitOn: "StartDate");
        
        return Result.Ok(rentals);
    }
}