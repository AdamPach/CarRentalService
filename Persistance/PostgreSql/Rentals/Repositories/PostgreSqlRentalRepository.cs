using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Domain.Rentals.Entities;
using CarRentalService.Domain.Rentals.ValueObjects;
using CarRentalService.Domain.Vehicles.Entities;
using CarRentalService.Domain.Vehicles.ValueObjects;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.Persistence.PostgreSql.Rentals.Mappers;
using CarRentalService.UseCases.Rentals.Repositories;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Rentals.Repositories;

public class PostgreSqlRentalRepository : IRentalRepository
{
    private readonly DatabaseConnectionFactory _connectionFactory;
    private readonly RentalCriteriaToSqlMapper _criteriaToSqlMapper;

    public PostgreSqlRentalRepository(
        DatabaseConnectionFactory connectionFactory, 
        RentalCriteriaToSqlMapper criteriaToSqlMapper)
    {
        _connectionFactory = connectionFactory;
        _criteriaToSqlMapper = criteriaToSqlMapper;
    }
    
    private const string QueryTemplateWithCustomerAndVehicle = 
        @"SELECT ""Rentals"".""Id"", ""Status"", ""TotalPrice"", ""CustomerId"", ""VehicleId"", ""EmployeeId"",
          ""StartDate"", ""EndDate"", ""ReturnDate"",
            ""Person"".*, ""Vehicle"".*, ""Vehicle"".""BrandName"" AS ""Name""
          FROM ""Rentals""
          INNER JOIN ""Customer"" ON ""Customer"".""Id"" = ""Rentals"".""CustomerId""
          INNER JOIN ""Person"" ON ""Person"".""Id"" = ""Customer"".""Id""
          INNER JOIN ""Vehicle"" ON ""Vehicle"".""Id"" = ""Rentals"".""VehicleId""
          /**where**/";
    public async Task<Result<IEnumerable<Rental>>> GetAllWithCustomerAndVehicleAsync(RentalCriteria criteria)
    {
        var mapperResult = _criteriaToSqlMapper.Map(criteria);
        
        if (mapperResult.IsFailed)
        {
            return Result.Fail<IEnumerable<Rental>>("Mapping criteria to SQL failed");
        }
        
        var sqlBuilder = mapperResult.Value;
        
        var query = sqlBuilder.AddTemplate(QueryTemplateWithCustomerAndVehicle);
        
        await using var connection = await _connectionFactory.CreateConnection();

        var rentals = await connection
            .QueryAsync<Rental, RentalDateRange, Customer, Vehicle, Manufacturer,Rental>(
                query.RawSql, 
                param: query.Parameters,
                map: (rental, range, customer, vehicle, manufacturer) =>
                {
                    vehicle.Manufacturer = manufacturer;
                    rental.RentalDateRange = range;
                    rental.Customer = customer;
                    rental.Vehicle = vehicle;
                    return rental;
                }, splitOn: "Id,StartDate,Id,Id,Name");
        
        return Result.Ok(rentals);
    }

    public async Task<Result<Rental>> GetByIdWithCustomerAndVehicleAsync(Guid rentalId)
    {
        var criteria = new RentalCriteria { RentalId = rentalId };
        
        var result = await GetAllWithCustomerAndVehicleAsync(criteria);
        
        if(result.IsFailed)
        {
            return Result.Fail<Rental>("Failed to get rental");
        }
        
        var rentals = result.Value.ToList();

        return rentals.Count switch
        {
            0 => Result.Fail<Rental>("Rental not found"),
            > 1 => Result.Fail<Rental>("More than one rental found"),
            _ => Result.Ok(rentals.Single())
        };
    }

    public async Task<Result<Rental>> CreateRentalAsync(Rental rental)
    {
        rental.Id = Guid.NewGuid();
        
        var parameters = new
        {
            rental.Id,
            rental.Status,
            rental.TotalPrice,
            rental.CustomerId,
            rental.VehicleId,
            rental.EmployeeId,
            rental.RentalDateRange.StartDate,
            rental.RentalDateRange.EndDate,
        };
        
        const string insertCommand = 
            @"INSERT INTO ""Rentals"" (""Id"", ""Status"", ""TotalPrice"", ""CustomerId"", ""VehicleId"", ""EmployeeId"",""StartDate"", ""EndDate"")
              VALUES (@Id, @Status, @TotalPrice, @CustomerId, @VehicleId, @EmployeeId, @StartDate, @EndDate)";
        
        var connection = await _connectionFactory.CreateConnection();
        
        await using var transaction = await connection.BeginTransactionAsync();
        
        try
        {
            await connection.ExecuteAsync(insertCommand, parameters);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return Result.Fail("Failed to create rental");
        }

        await transaction.CommitAsync();
        return rental;
    }

    public async Task<Result> UpdateRentalAsync(Rental rental)
    {
        var parameters = new
        {
            rental.Id,
            rental.Status,
            rental.TotalPrice,
            rental.CustomerId,
            rental.VehicleId,
            rental.EmployeeId,
            rental.RentalDateRange.StartDate,
            rental.RentalDateRange.EndDate,
            rental.RentalDateRange.ReturnDate,
        };

        const string updateCommand = @"
            UPDATE ""Rentals""
            SET ""Status"" = @Status, ""TotalPrice"" = @TotalPrice, ""CustomerId"" = @CustomerId, ""VehicleId"" = @VehicleId, 
                ""EmployeeId"" = @EmployeeId, ""StartDate"" = @StartDate, ""EndDate"" = @EndDate, ""ReturnDate"" = @ReturnDate
            WHERE ""Id"" = @Id";
        
        await using var connection = await _connectionFactory.CreateConnection();
        
        try
        {
            await connection.ExecuteAsync(updateCommand, parameters);
        }
        catch (Exception)
        {
            return Result.Fail("Failed to create rental");
        }
        
        return Result.Ok();
    }
}