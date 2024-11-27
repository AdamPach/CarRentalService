using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Persons.ValueObjects;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.UseCases.Persons.Customers.Repository;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Persons.Repositories;

internal class PostgreSqlCustomerRepository : ICustomerRepository
{
    private readonly DatabaseConnectionFactory _connectionFactory;
    

    public PostgreSqlCustomerRepository(
        DatabaseConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    private const string QueryTemplate = @"SELECT * FROM ""Customer"" /**innerjoin**//**where**/";

    public async Task<Result<IEnumerable<Customer>>> GetAllAsync(CustomerCriteria criteria)
    {
        await using var connection = await _connectionFactory
            .CreateConnection();
        
        var sqlBuilder = new SqlBuilder();
        
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
}