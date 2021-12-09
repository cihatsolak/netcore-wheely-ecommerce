using System;
using System.Threading.Tasks;
using Wheely.Core.Enums;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.Redis
{
    public partial interface IRedisService : IDisposable
    {
        /// <summary>
        /// Redis database connection
        /// </summary>
        void ConnectServer();

        /// <summary>
        /// Clear app cache
        /// </summary>
        /// <returns></returns>
        Task ClearAppCacheAsync();

        /// <summary>
        /// Increment
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="increment">increment value</param>
        void Increment(string cacheKey, int increment = 1);

        /// <summary>
        /// Increment
        /// </summary>
        /// <param name="cacheKey">cache key</param>
        /// <param name="increment">increment value</param>
        /// <returns></returns>
        Task IncrementAsync(string cacheKey, int increment = 1);

        /// <summary>
        /// Returns true and data if cached data exists, false and null otherwise (synchronous)
        /// </summary>
        /// <typeparam name="TModel">class</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <param name="value">object</param>
        /// <returns>type of boolean and object</returns>
        IResult TryGetValue<TModel>(string cacheKey, out TModel value);

        /// <summary>
        /// Get data from cache (synchronous)
        /// </summary>
        /// <typeparam name="TModel">data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <returns>type of TModel</returns>
        IDataResult<TModel> Get<TModel>(string cacheKey);

        /// <summary>
        /// Get data from cache (asynchronous) 
        /// </summary>
        /// <typeparam name="TModel">data type</typeparam>
        /// <param name="cacheKey">cache key</param>
        /// <returns>type of TModel</returns>
        Task<IDataResult<TModel>> GetAsync<TModel>(string cacheKey);

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

        /// <summary>
        /// Remove keys by search key
        /// </summary>
        /// <param name="searchKey">redis key pattern</param>
        /// <param name="keySearchType">key search type</param>
        void RemoveKeysBySearchKey(string searchKey, KeySearchType keySearchType);

        /// <summary>
        /// Remove keys by search key
        /// </summary>
        /// <param name="searchKey">redis key pattern</param>
        /// <returns></returns>
        Task RemoveKeysBySearchKeyAsync(string searchKey, KeySearchType keySearchType);
    }
}
