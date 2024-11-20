using CarRentalService.Domain.Common.Interfaces;
using CarRentalService.UseCases.Common.Specification;

namespace CarRentalService.Persistence.Common;

internal class EmptySpecification<T> : ISpecification<T>
    where T : IEntity
{
    public Func<T, bool> IsSatisfiedBy { get; } = _ => true;
    public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
}