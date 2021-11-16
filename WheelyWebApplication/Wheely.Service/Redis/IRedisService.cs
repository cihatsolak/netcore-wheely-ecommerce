using System.Threading.Tasks;
using Wheely.Core.Enums;

namespace Wheely.Service.Redis
{
    public partial interface IRedisService
    {
        /// <summary>
        /// Returns true and data if cached data exists, false and null otherwise (synchronous)
        /// </summary>
        /// <typeparam name="TModel">class</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">object</param>
        /// <returns>type of boolean and object</returns>
        bool TryGetValue<TModel>(string cacheKey, out TModel value);

        /// <summary>
        /// Get data from cache (synchronous)
        /// </summary>
        /// <typeparam name="TModel">data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <returns>type of TModel</returns>
        TModel Get<TModel>(string cacheKey);

        /// <summary>
        /// Get data from cache (asynchronous) 
        /// </summary>
        /// <typeparam name="TModel">data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <returns>type of TModel</returns>
        Task<TModel> GetAsync<TModel>(string cacheKey);

        /// <summary>
        /// insert data to cache (synchronous)
        /// </summary>
        /// <typeparam name="TModel">data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">data</param>
        /// <param name="slidingExpiration">sliding expiration minute, default: three minute</param>
        /// <param name="absoluteExpiration">absolute expiration minute, default: twenty minute</param>
        void Set<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes);

        /// <summary>
        /// insert data to cache (asynchronous)
        /// </summary>
        /// <typeparam name="TModel">data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">data</param>
        Task SetAsync<TModel>(string cacheKey, TModel value);

        /// <summary>
        /// insert data to cache (asynchronous) 
        /// </summary>
        /// <typeparam name="TModel">data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">data</param>
        /// <param name="slidingExpiration">sliding expiration minute, default: three minute</param>
        /// <param name="absoluteExpiration">absolute expiration minute, default: twenty minute</param>
        Task SetAsync<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes);

        /// <summary>
        /// Delete data from cache (synchronous)
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        void Remove(string cacheKey);

        /// <summary>
        /// Delete data from cache (asynchronous) 
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        Task RemoveAsync(string cacheKey);
    }
}
