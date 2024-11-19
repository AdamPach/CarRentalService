using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.UseCases.Common.Specification;

namespace CarRentalService.Persistence.Common;

internal class EmptySpecification<T> : ISpecification<T>
    where T : IEntity
{
    public Func<T, bool> IsSatisfiedBy { get; } = _ => true;
    public IParameterComponent<T> Parameters { get; } = new ParameterComposite<T>(ParameterCompositeOperator.And);
}