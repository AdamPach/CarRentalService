using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Domain.Persons.ValueObjects;
using CarRentalService.Persistence.Common;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.Persistence.PostgreSql.Mappers;
using CarRentalService.UseCases.Common.Specification;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.DataMappers;

internal class CustomerDataMapper : IDataMapper<Customer>
{
    private readonly SqlSpecificationMapper<Customer> _mapper;
    private readonly DatabaseConnectionFactory _connectionFactory;

    public CustomerDataMapper(
        SqlSpecificationMapper<Customer> mapper, 
        DatabaseConnectionFactory databaseConnectionFactory)
    {
        _mapper = mapper;
        _connectionFactory = databaseConnectionFactory;
    }

    private const string SelectTemplate = @"SELECT * FROM ""Customer"" ""c"" /**innerjoin**/  /**where**/";

    public async Task<Result<IEnumerable<Customer>>> Select(ISpecification<Customer> specification)
    {
        var queryBuilder = new SqlBuilder();
        
        queryBuilder.InnerJoin(@"""Person"" ""p"" ON ""c"".""PersonId"" = ""p"".""Id""");
        
        var select = queryBuilder.AddTemplate(SelectTemplate); 
        
        await using var connection = await _connectionFactory.CreateConnection();
        
        var customers = await connection.QueryAsync<Customer, Address, Customer>(
            select.RawSql,
            (customer, address) =>
            {
                customer.Address = address;
                return customer;
            }, splitOn: "Street");
        
        return Result.Ok(customers);
    }

    public Task<Result<Customer>> Insert(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Customer>> Update(Customer entity)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delete(Customer entity)
    {
        throw new NotImplementedException();
    }
}