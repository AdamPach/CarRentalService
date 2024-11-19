using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.UseCases.Common.Specification;
using FluentResults;

namespace CarRentalService.UseCases.Common;

public interface IRepository<T>
    where T : IEntity
{
    Task<Result<IEnumerable<T>>> GetAllAsync();
    Task<Result<T>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<T>>> FindAsync(ISpecification<T> specification);
    Task<Result<T>> AddAsync(T entity);
    Task<Result<T>> UpdateAsync(Guid id, T entity);
    Task<Result> DeleteAsync(Guid id);
}