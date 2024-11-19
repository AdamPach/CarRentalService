using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.UseCases.Common.Specification;

namespace CarRentalService.Persistence.Common;

internal class IdSpecification<T>(Guid id) : ISpecification<T>
    where T : IEntity
{
    public Func<T, bool> IsSatisfiedBy => entity => Parameters.IsSatisfiedBy(entity).IsSuccess;
    public IParameterComponent<T> Parameters { get; } = new Parameter<T>("Id", id);
}