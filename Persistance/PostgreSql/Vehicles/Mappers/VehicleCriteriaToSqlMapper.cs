using CarRentalService.Domain.Rentals.Enums;
using CarRentalService.Domain.Vehicles.Criteria;
using CarRentalService.Persistence.PostgreSql.Common;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Vehicles.Mappers;

public class VehicleCriteriaToSqlMapper : CriteriaToSqlMapper<VehicleCriteria>
{
    private const string ExcludeRented =
        @"NOT EXISTS(
          SELECT 1 FROM ""Rentals"" R
          WHERE R.""VehicleId"" = ""Vehicle"".""Id""
            AND R.""Status"" = @Status)";
    
    public override Result<SqlBuilder> Map(VehicleCriteria from)
    {
        var result = base.Map(from);
        
        if (result.IsFailed)
        {
            return result;
        }
        
        var sqlBuilder = result.Value;

        if (from.ExcludeRented)
        {
            sqlBuilder.Where(ExcludeRented, 
                new { Status = RentalStatus.Active });
        }

        return sqlBuilder;
    }
}