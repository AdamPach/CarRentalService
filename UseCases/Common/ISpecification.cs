using CarRentalService.Domain.Entities.Common.Interfaces;

namespace CarRentalService.UseCases.Common;

public interface ISpecification<T>
    where T : IEntity
{
    bool IsSatisfiedBy(T entity);
    IDictionary<string, object> Parameters { get; }
}