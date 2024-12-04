using CarRentalService.Domain.Common.Criteria;
using CarRentalService.UseCases.Common;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Common;

public abstract class CriteriaToSqlMapper<TCriteria> : IMapper<TCriteria, SqlBuilder>
    where TCriteria : CriteriaBase
{
    public virtual Result<SqlBuilder> Map(TCriteria from)
    {
        var sqlBuilder = new SqlBuilder();
        
        return Result.Ok(sqlBuilder);
    }
}