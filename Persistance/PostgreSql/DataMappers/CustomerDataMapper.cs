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
    private readonly SpecificationToSqlBuilderMapper<Customer> _mapper;
    private readonly DatabaseConnectionFactory _connectionFactory;

    public CustomerDataMapper(
        SpecificationToSqlBuilderMapper<Customer> mapper, 
        DatabaseConnectionFactory databaseConnectionFactory)
    {
        _mapper = mapper;
        _connectionFactory = databaseConnectionFactory;
    }

    private const string SelectTemplate = 
        @"SELECT * FROM ""Customer"" /**innerjoin**//**where**/";
    public async Task<Result<IEnumerable<Customer>>> Select(ISpecification<Customer> specification)
    {
        var queryBuilderResult = _mapper.Map(specification);

        if (queryBuilderResult.IsFailed)
        {
            return Result.Fail(queryBuilderResult.Errors);
        }
            
        queryBuilderResult.Value.InnerJoin(@"""Person"" ON ""Customer"".""Id"" = ""Person"".""Id""");
        
        var select = queryBuilderResult.Value.AddTemplate(SelectTemplate); 
        
        await using var connection = await _connectionFactory.CreateConnection();
        
        var customers = await connection.QueryAsync<Customer, Address, Customer>(
            select.RawSql,
            param: select.Parameters,
            map:(customer, address) =>
            {
                customer.Address = address;
                return customer;
            },splitOn: "Street");
        
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