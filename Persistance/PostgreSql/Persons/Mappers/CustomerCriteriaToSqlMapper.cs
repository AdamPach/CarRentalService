using CarRentalService.Domain.Persons.Criteria;
using CarRentalService.Persistence.PostgreSql.Common;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Persons.Mappers;

internal sealed class CustomerCriteriaToSqlMapper : CriteriaToSqlMapper<CustomerCriteria>
{
    public override Result<SqlBuilder> Map(CustomerCriteria from)
    {
        var sqlBuilder = base.Map(from).Value;
        
        if (!string.IsNullOrWhiteSpace(from.FirstName))
        {
            sqlBuilder.Where(@"""FirstName"" = @FirstName", new { from.FirstName });
        }
        
        return Result.Ok(sqlBuilder);
    }
}