using CarRentalService.Domain.Common.Interfaces;

namespace CarRentalService.UseCases.Common.Specification;

public interface ISpecification<T>
    where T : IEntity
{
    Func<T, bool> IsSatisfiedBy { get; }
    IDictionary<string, object> Parameters { get; }
}