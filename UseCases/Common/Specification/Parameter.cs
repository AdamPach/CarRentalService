using CarRentalService.Domain.Entities.Common.Interfaces;
using FluentResults;

namespace CarRentalService.UseCases.Common.Specification;

public class Parameter<T>(string name, object value) : IParameterComponent<T>
    where T : IEntity
{
    public readonly string Name = name;
    public readonly object Value = value;

    public Result<bool> IsSatisfiedBy(T entity)
    {
        dynamic entityValue = entity;
        IDictionary<string, object?> properties = entityValue;
        
        return Result.Ok(properties[Name] == Value);
    }

    Result<IParameterComponent<T>> IParameterComponent<T>.Next()
    {
        return Result.Fail("Parameter is not a composite.");
    }
}