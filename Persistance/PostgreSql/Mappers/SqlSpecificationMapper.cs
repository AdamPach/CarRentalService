using System.Text;
using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.Persistence.PostgreSql.Models;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Common.Specification;
using FluentResults;

namespace CarRentalService.Persistence.PostgreSql.Mappers;

internal class SqlSpecificationMapper<T> : IMapper<IParameterComponent<T>, SqlQueryParams>
    where T : IEntity
{
    public Result<SqlQueryParams> Map(IParameterComponent<T> from)
    {
        var result = from.Next();

        if (result.IsFailed && from is Parameter<T> param)
        {
            return GetParameterString(param);
        }

        var queryBuilder = new StringBuilder(1024);
        var parameters = new List<KeyValuePair<string, string>>();

        while (result.IsSuccess)
        {
            var mapResult = Map(result.Value);

            if (mapResult.IsFailed)
            {
                return mapResult;
            }

            queryBuilder.Append(mapResult.Value.Query);
            parameters.AddRange(mapResult.Value.Parameters);
            
            result = from.Next();
        }

        return new SqlQueryParams(queryBuilder.ToString(), new Dictionary<string, string>(parameters));
    }

    private Result<SqlQueryParams> GetParameterString(Parameter<T> parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter.Value.ToString()))
        {
            return Result.Fail("Parameter is not assigned");
        }
        
        var sb = new StringBuilder(1024);

        sb.Append(" AND ");
        sb.Append(parameter.Name);
        sb.Append(" = ");
        sb.Append('@');
        sb.Append(parameter.Name);

        var parameters = new Dictionary<string, string>
        {
            { parameter.Name, parameter.Value.ToString()! }
        };
        
        return new SqlQueryParams(sb.ToString(), parameters);
    }
}