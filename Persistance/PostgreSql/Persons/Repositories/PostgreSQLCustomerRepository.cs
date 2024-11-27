using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Persons.ValueObjects;
using CarRentalService.Persistence.PostgreSql.Common;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.UseCases.Persons.Customers.Repository;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Persons.Repositories;

internal class PostgreSqlCustomerRepository : ICustomerRepository
{
    private readonly DatabaseConnectionFactory _connectionFactory;
    private readonly CriteriaToSqlMapper<CustomerCriteria> _criteriaToSqlMapper;
    public PostgreSqlCustomerRepository(
        DatabaseConnectionFactory connectionFactory, 
        CriteriaToSqlMapper<CustomerCriteria> criteriaToSqlMapper)
    {
        _connectionFactory = connectionFactory;
        _criteriaToSqlMapper = criteriaToSqlMapper;
    }
    
    private const string QueryTemplate = @"SELECT * FROM ""Customer"" /**innerjoin**//**where**/";

    public async Task<Result<IEnumerable<Customer>>> GetAllAsync(CustomerCriteria criteria)
    {
        await using var connection = await _connectionFactory
            .CreateConnection();

        var sqlBuilder = _criteriaToSqlMapper.Map(criteria).Value;
        
        sqlBuilder.InnerJoin(@"""Person"" ON ""Person"".""Id"" = ""Customer"".""Id""");
        
        var query = sqlBuilder.AddTemplate(QueryTemplate);
        
        var customers = await connection.QueryAsync<Customer, Address, Customer>(
            query.RawSql,
            param: query.Parameters,
            map:(customer, address) =>
            {
                customer.Address = address;
                return customer;
            },splitOn: "Street");

        return Result.Ok(customers);
    }

    public async Task<Result<Customer>> GetByIdAsync(Guid id)
    {
        await using var connection = await _connectionFactory
            .CreateConnection();
        
        var sqlBuilder = new SqlBuilder();
        
        sqlBuilder.InnerJoin(@"""Person"" ON ""Person"".""Id"" = ""Customer"".""Id""");
        sqlBuilder.Where(@"""Customer"".""Id"" = @Id", new { Id = id });
        
        var query = sqlBuilder.AddTemplate(QueryTemplate);

        var customers = await connection.QueryAsync<Customer, Address, Customer>(
            query.RawSql,
            param: query.Parameters,
            map: (customer, address) =>
            {
                customer.Address = address;
                return customer;
            }, splitOn: "Street");
        
        var customer = customers.SingleOrDefault();
        
        return customer == null
            ? Result.Fail<Customer>($"Customer with id {id} not found")
            : Result.Ok(customer);
    }

    public async Task<Result<Customer>> CreateAsync(Customer customer)
    {
        const string insertPersonSql = 
            @"INSERT INTO ""Person"" (""Id"", ""FirstName"", ""LastName"", ""DateOfBirth"", ""PhoneNumber"", ""Street"",  ""City"",""ZipCode"")
              VALUES (@Id, @FirstName, @LastName, @DateOfBirth, @PhoneNumber, @Street, @City, @ZipCode)";
        
        var personParams = new
        {
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.DateOfBirth,
            customer.PhoneNumber,
            customer.Address!.Street,
            customer.Address!.City,
            customer.Address!.ZipCode
        };
        
        const string insertCustomerSql =
            @"INSERT INTO ""Customer"" (""Id"", ""LicenseNumber"", ""RegistrationDate"")
              VALUES (@Id, @LicenseNumber, @RegistrationDate)";
        
        var customerParams = new
        {
            customer.Id,
            customer.LicenseNumber,
            customer.RegistrationDate
        };
        
        await using var connection = await _connectionFactory
            .CreateConnection();

        var transaction = await connection.BeginTransactionAsync();

        try
        {
            await connection.ExecuteAsync(insertPersonSql, personParams);
            await connection.ExecuteAsync(insertCustomerSql, customerParams);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return Result.Fail<Customer>("Error creating customer");
        }
        
        await transaction.CommitAsync();
        return Result.Ok(customer);
    }
}