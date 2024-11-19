using CarRentalService.Domain.Entities.Common.Interfaces;
using FluentResults;

namespace CarRentalService.UseCases.Common.Specification;

public class ParameterComposite<T>(ParameterCompositeOperator compositeOperator) : IParameterComponent<T>
    where T : IEntity
{
    public ParameterCompositeOperator CompositeOperator { get; } = compositeOperator;
    
    private readonly List<IParameterComponent<T>> _parameters = [];
    
    private int _index;
    
    public ParameterComposite<T> AddParameter(IParameterComponent<T> parameter)
    {
        _parameters.Add(parameter);

        return this;
    }

    public Result<bool> IsSatisfiedBy(T entity)
    {
        return _parameters
            .All( p => p.IsSatisfiedBy(entity).IsSuccess);
    }

    Result<IParameterComponent<T>> IParameterComponent<T>.Next()
    {
        return _index < _parameters.Count 
            ? Result.Ok(_parameters[_index++]) 
            : Result.Fail("No more parameters.");
    }
}