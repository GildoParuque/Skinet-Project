using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inpuQuery, 
        ISpecification<TEntity> spec)
    {
        var query = inpuQuery;

        if(spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); //p => p.ProductTypeId
        }

        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    } 
}
