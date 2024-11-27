using CarRentalService.Domain.Common.Criteria;
using CarRentalService.Domain.Common.Interfaces;
using FluentResults;

namespace CarRentalService.UseCases.Common;

public interface IReadRepository<TEntity, in TCriteria>
    where TEntity : IEntity
    where TCriteria : CriteriaBase
{
    Task<Result<IEnumerable<TEntity>>> GetAllAsync(TCriteria criteria);
    Task<Result<TEntity>> GetByIdAsync(Guid id);
}