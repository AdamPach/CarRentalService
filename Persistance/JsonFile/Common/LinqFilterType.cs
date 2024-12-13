using System.Linq.Expressions;
using CarRentalService.Domain.Common.Interfaces;

namespace CarRentalService.Persistence.JsonFile.Common;

public class LinqFilterType<T>
    where T : IEntity
{
    private readonly List<Expression<Func<T, bool>>> _whereExpressions = new();
    
    public void AddWhereExpression(Expression<Func<T, bool>> expression)
    {
        _whereExpressions.Add(expression);
    }

    public IEnumerable<T> Apply(IQueryable<T> entities)
    {
        foreach (var whereExpression in _whereExpressions)
        {
            entities = entities.Where(whereExpression);
        }  
        
        return entities.ToList();
    }
}