using System.Linq;

namespace Querying
{
    public static class QueryableExtensions
    {
        public static IQueryable<string> ToLower(this IQueryable<string> values)
        {
            return values.Select(p => p.ToLower());
        }
    }
}