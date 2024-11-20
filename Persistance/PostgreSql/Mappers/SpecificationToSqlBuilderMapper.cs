using System.Dynamic;
using CarRentalService.Domain.Common.Interfaces;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Common.Specification;
using Dapper;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Mappers;

internal class SpecificationToSqlBuilderMapper<T> : IMapper<ISpecification<T>, SqlBuilder>
    where T : IEntity
{
    public Result<SqlBuilder> Map(ISpecification<T> from)
    {
        var sqlBuilder = new SqlBuilder();

        foreach (var parameter in from.Parameters)
        {
            Dictionary<string, object> parameters = [];

            parameters[parameter.Key] = parameter.Value;

            sqlBuilder.Where(@$"""{typeof(T).Name}"".""{parameter.Key}"" = @{parameter.Key}", parameters);
        };

        return sqlBuilder;
    }
}