using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Wheely.Core.Entities.Abstract;

namespace Wheely.Data.Concrete.Extensions
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
        public static IQueryable<T> Includes<T>(this IQueryable<T> query, params string[] includeProperties) where T : class, IEntity, new()
        {
            if (query.IsNullOrNotAny())
                return query;

            
            return includeProperties.Aggregate(query, EntityFrameworkQueryableExtensions.Include); ;
        }

        /// <summary>
        /// Bulk add to collection type
        /// </summary>
        /// <typeparam name="T">entity type</typeparam>
        /// <param name="collection">type of collection interface</param>
        /// <param name="items">type of ienumerable interface</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items) where T : class, new()
        {
            foreach (var item in items)
                collection.Add(item);
        }

        /// <summary>
        /// Is null or not any
        /// </summary>
        /// <typeparam name="T">type of enumerable interface</typeparam>
        /// <param name="source">system collections generic</param>
        /// <returns>type of boolean</returns>
        public static bool IsNullOrNotAny<T>(this IEnumerable<T> source) where T : class, new()
        {
            return !(source?.Any() ?? false);
        }
    }
}
