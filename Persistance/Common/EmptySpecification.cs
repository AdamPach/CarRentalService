using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.UseCases.Common;

namespace CarRentalService.Persistence.Common;

public class EmptySpecification<T> : ISpecification<T>
    where T : IEntity
{
    public bool IsSatisfiedBy(T entity)
    {
        return true;
    }

    public IDictionary<string, object> Parameters { get; } 
        = new Dictionary<string, object>();
}