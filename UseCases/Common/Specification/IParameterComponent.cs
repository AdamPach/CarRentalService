using CarRentalService.Domain.Entities.Common.Interfaces;
using FluentResults;

namespace CarRentalService.UseCases.Common.Specification;

public interface IParameterComponent<T>
    where T : IEntity
{
    Result<bool> IsSatisfiedBy(T entity);
    Result<IParameterComponent<T>> Next();
}