using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Persistence.PostgreSql.Common;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Persons.Mappers;

internal sealed class EmployeeCriteriaToSqlMapper : CriteriaToSqlMapper<EmployeeCriteria>
{
    public override Result<SqlBuilder> Map(EmployeeCriteria from)
    {
        var sqlBuilder =  base.Map(from).Value;

        if (!string.IsNullOrWhiteSpace(from.EmployeeNumber))
        {
            sqlBuilder.Where(@"""EmployeeNumber"" = @EmployeeNumber", new { from.EmployeeNumber });
        }

        return sqlBuilder;
    }
}