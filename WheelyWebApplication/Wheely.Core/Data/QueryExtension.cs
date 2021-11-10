using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        /// <summary>
        /// Bulk add to collection type
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="collection">type of collection interface</param>
        /// <param name="items">type of ienumerable interface</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }
    }
}
