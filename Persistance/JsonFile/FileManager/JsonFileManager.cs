using System.Text.Json;
using CarRentalService.Domain.Common.Interfaces;
using FluentResults;

namespace CarRentalService.Persistence.JsonFile.FileManager;

public class JsonFileManager<TEntity>
    where TEntity : IEntity
{
    private readonly SemaphoreSlim _semaphoreSlim = new(1);
    
    public async Task<Result> WriteToFileAsync(IEnumerable<TEntity> entities)
    {
        await _semaphoreSlim.WaitAsync();
        
        try
        {
            var json = JsonSerializer.Serialize(entities);
            await File.WriteAllTextAsync($"{typeof(TEntity).Name}.json", json);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
        finally
        {
            _semaphoreSlim.Release();
        }

        return Result.Ok();
    }   
    
    public async Task<Result<IEnumerable<TEntity>>> ReadFromFileAsync()
    {
        await _semaphoreSlim.WaitAsync();
        
        try
        {
            var json = await File.ReadAllTextAsync($"{typeof(TEntity).Name}.json");
            var entities = JsonSerializer.Deserialize<IEnumerable<TEntity>>(json);
            return Result.Ok(entities ?? []);
        }
        catch (Exception e)
        {
            return Result.Fail<IEnumerable<TEntity>>(e.Message);
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
}