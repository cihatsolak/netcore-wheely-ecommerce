using Microsoft.EntityFrameworkCore;
using System.Linq;
using Wheely.Core.Entities.Abstract;

namespace Wheely.Core.Data
{
    public static class QueryExtension
    {
        /// <summary>
        /// Query with dynamic Include
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="context">dbContext</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns>Constructed query with include properties</returns>
        public static IQueryable<T> Includes<T>(this IQueryable<T> query, params string[] includeProperties) where T : class, IEntity
        {
            foreach (string include in includeProperties)
                query = query.Include(include);

            return query;
        }
    }
}
