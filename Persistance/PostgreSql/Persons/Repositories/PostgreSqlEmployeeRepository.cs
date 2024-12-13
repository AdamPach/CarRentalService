using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Domain.Persons.Entities;
using CarRentalService.Persistence.PostgreSql.Common;
using CarRentalService.Persistence.PostgreSql.Database;
using CarRentalService.Persistence.PostgreSql.Extensions;
using CarRentalService.UseCases.Persons.Employees.Repository;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Persons.Repositories;

internal sealed class PostgreSqlEmployeeRepository : IEmployeeRepository
{
    private readonly DatabaseConnectionFactory _connectionFactory;
    private readonly CriteriaToSqlMapper<EmployeeCriteria> _sqlMapper;

    public PostgreSqlEmployeeRepository(
        DatabaseConnectionFactory connectionFactory,
        CriteriaToSqlMapper<EmployeeCriteria> sqlMapper)
    {
        _connectionFactory = connectionFactory;
        _sqlMapper = sqlMapper;
    }

    private const string QueryTemplate = @"SELECT * FROM ""Employee"" /**innerjoin**//**where**/";

    public async Task<Result<IEnumerable<Employee>>> GetAllAsync(EmployeeCriteria criteria)
    {
        var mapperResult = _sqlMapper.Map(criteria);

        if (mapperResult.IsFailed)
        {
            return Result.Fail(mapperResult.Errors);
        }

        var sqlBuilder = mapperResult.Value;

        sqlBuilder.InnerJoin(@"""Person"" ON ""Person"".""Id"" = ""Employee"".""Id""");

        var query = sqlBuilder.AddTemplate(QueryTemplate);
        
        await using var connection = 
            await _connectionFactory.CreateConnection();

        await using var reader = await connection.ExecuteReaderAsync(query.RawSql, query.Parameters);

        var fullTimeEmployeeReader = reader.GetRowParser<FullTimeEmployee>();
        var managerEmployeeReader = reader.GetRowParser<ManagerEmployee>();
        var employees = new List<Employee>();

        while (await reader.ReadAsync())
        {
            var discriminator = reader.GetInt32(reader.GetOrdinal("EmployeeType"));
            
            switch (discriminator)
            {
                case 0:
                    var manager = managerEmployeeReader(reader);
                    manager.Address = reader.MapAddress();
                    employees.Add(manager);
                    break;
                case 1:
                    var fullTimeEmployee = fullTimeEmployeeReader(reader);
                    fullTimeEmployee.Address = reader.MapAddress();
                    employees.Add(fullTimeEmployee);
                    break;
            }
        }

        return employees;
    }
}