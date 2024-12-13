using CarRentalService.Domain.Common.Criteria;
using CarRentalService.Domain.Common.Interfaces;
using CarRentalService.UseCases.Common;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.Common;

public abstract class CriteriaToLinqMapper<TEntity, TCriteria> : IMapper<TCriteria, LinqFilterType<TEntity>>
    where TEntity : IEntity
    where TCriteria : CriteriaBase
{
    public virtual Result<LinqFilterType<TEntity>> Map(TCriteria from)
    {
        var filter = new LinqFilterType<TEntity>();

        return filter;
    }
}