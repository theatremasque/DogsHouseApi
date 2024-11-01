using System.Linq.Expressions;

namespace DogsHouse.API.QueryExtensions;

public static class SortExtension
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, string order, Expression<Func<T, object>> expression) where T : class
    {
        query = order.ToLower() switch
        {
            "asc" => query.OrderBy(expression),
            "desc" => query.OrderByDescending(expression),
            _ => query 
        };
            
        return query;
    }
}