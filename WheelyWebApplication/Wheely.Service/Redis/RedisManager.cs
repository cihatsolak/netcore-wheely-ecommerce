using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using Wheely.Core.Enums;
using Wheely.Core.Utilities;

namespace Wheely.Service.Redis
{
    public sealed class RedisManager : IRedisService
    {
        #region Fields
        private readonly IDistributedCache _distributedCache;
        #endregion

        #region Constructor
        public RedisManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        #endregion

        #region Methods
        public bool TryGetValue<TModel>(string cacheKey, out TModel value)
        {
            value = default;

            if (string.IsNullOrWhiteSpace(cacheKey)) return false;

            string cachedValue = _distributedCache.GetString(cacheKey.ToLower());
            if (string.IsNullOrWhiteSpace(cachedValue))
                return default;

            value = cachedValue.AsModel<TModel>();
            return true;
        }

        public TModel Get<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return default;

            string cachedValue = _distributedCache.GetString(cacheKey.ToLower());
            if (string.IsNullOrWhiteSpace(cachedValue))
                return default;

            return cachedValue.AsModel<TModel>();
        }

        public async Task<TModel> GetAsync<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return default;

            string cachedValue = await _distributedCache.GetStringAsync(cacheKey.ToLower());
            if (string.IsNullOrWhiteSpace(cachedValue))
                return default;

            return cachedValue.AsModel<TModel>();
        }

        public void Set<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return;

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpiration.ToInt()),
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration.ToInt())
            };

            _distributedCache.SetString(cacheKey.ToLower(), value.ToJsonString(), distributedCacheEntryOptions);
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return;

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpiration.ToInt()),
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration.ToInt())
            };

            await _distributedCache.SetStringAsync(cacheKey.ToLower(), value.ToJsonString(), distributedCacheEntryOptions);
        }

        public void Remove(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return;

            _distributedCache.Remove(cacheKey.ToLower());
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return;

            await _distributedCache.RemoveAsync(cacheKey.ToLower());
        }
        #endregion
    }
}
