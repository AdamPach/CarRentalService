using CarRentalService.Domain.Entities.Common.Interfaces;

namespace CarRentalService.UseCases.Common.Specification;

public interface ISpecification<T>
    where T : IEntity
{
    Func<T, bool> IsSatisfiedBy { get; }
    IParameterComponent<T> Parameters { get; }
}