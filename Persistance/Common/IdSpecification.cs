using CarRentalService.Domain.Common.Interfaces;
using CarRentalService.UseCases.Common.Specification;

namespace CarRentalService.Persistence.Common;

internal class IdSpecification<T>(Guid id) : ISpecification<T>
    where T : IEntity
{
    public Func<T, bool> IsSatisfiedBy => entity => entity.Id == id;
    public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object> { { "Id", id } };
}