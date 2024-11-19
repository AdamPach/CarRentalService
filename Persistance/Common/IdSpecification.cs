using CarRentalService.Domain.Entities.Common.Interfaces;
using CarRentalService.UseCases.Common;

namespace CarRentalService.Persistence.Common;

internal class IdSpecification<T>(Guid id) : ISpecification<T>
    where T : IEntity
{
    private readonly Guid _id = id;
    
    public bool IsSatisfiedBy(T entity) => entity.Id == _id;

    public IDictionary<string, object> Parameters { get; } = new Dictionary<string, object>
    {
        { "Id", id }
    };
}