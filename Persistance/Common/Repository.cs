using CarRentalService.Domain.Common.Interfaces;
using CarRentalService.UseCases.Common;
using CarRentalService.UseCases.Common.Specification;
using FluentResults;

namespace CarRentalService.Persistence.Common;

internal abstract class Repository<T>(IDataMapper<T> dataMapper) : IRepository<T>
    where T : IEntity
{
    protected readonly IDataMapper<T> DataMapper = dataMapper;

    public async Task<Result<IEnumerable<T>>> GetAllAsync()
    {
        var specification = new EmptySpecification<T>();
        
        return await DataMapper.Select(specification);
    }

    public async Task<Result<T>> GetByIdAsync(Guid id)
    {
        var specification = new IdSpecification<T>(id);
        
        var result =  await DataMapper.Select(specification);
        
        return GetSingleEntity(result);
    }

    private Result<T> GetSingleEntity(Result<IEnumerable<T>> result)
    {
        if (result.IsFailed)
        {
            return Result.Fail<T>("Searching for entity failed");
        }
        
        var count = result.Value.Count();
        
        if (count == 0)
        {
            return Result.Fail<T>("Entity not found");
        }
        
        if (count > 1)
        {
            return Result.Fail<T>("More than one entity found");
        }
        
        return result.Value.Single();
    }

    public async Task<Result<IEnumerable<T>>> FindAsync(ISpecification<T> specification)
    {
        return await DataMapper.Select(specification);
    }

    public async Task<Result<T>> AddAsync(T entity)
    {
        return await DataMapper.Insert(entity);
    }

    public async Task<Result<T>> UpdateAsync(Guid id, T entity)
    {
        var result = await GetByIdAsync(id);
        
        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }
        
        return await DataMapper.Update(entity);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var result = await GetByIdAsync(id);
        
        if (result.IsFailed)
        {
            return Result.Fail(result.Errors);
        }
        
        return await DataMapper.Delete(result.Value);
    }
}