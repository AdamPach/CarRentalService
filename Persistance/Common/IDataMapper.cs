using CarRentalService.Domain.Common.Interfaces;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Common.Specification;
using FluentResults;

namespace CarRentalService.Persistence.Common;

internal interface IDataMapper<T>
    where T : IEntity
{
    Task<Result<IEnumerable<T>>> Select(ISpecification<T> specification);
    Task<Result<T>> Insert(T entity);
    Task<Result<T>> Update(T entity);
    Task<Result> Delete(T entity);
}