using CarRentalService.Domain.Rentals.Criteria;
using CarRentalService.Persistence.PostgreSql.Common;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Rentals.Mappers;

public class RentalCriteriaToSqlMapper : CriteriaToSqlMapper<RentalCriteria>
{
    public override Result<SqlBuilder> Map(RentalCriteria from)
    {
        var result = base.Map(from);
        
        if (result.IsFailed)
        {
            return result;
        }
        
        var sqlBuilder = result.Value;
        
        if(from.Status.HasValue)
        {
            sqlBuilder.Where(@"""Status"" = @Status", new { from.Status });
        }

        return sqlBuilder;
    }
}