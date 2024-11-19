using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.UseCases.Common;
using FluentResults;

namespace CarRentalService.Persistence.Common;

public interface IDataMapper<T>
    where T : IEntity
{
    Task<Result<IEnumerable<T>>> Select(ISpecification<T> specification);
    Task<Result<T>> Insert(T entity);
    Task<Result<T>> Update(T entity);
    Task<Result> Delete(T entity);
}