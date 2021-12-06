using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Wheely.Core.Enums;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Core.Utilities;

namespace Wheely.Service.Redis
{
    public sealed class RedisManager : IRedisService
    {
        #region Fields
        private readonly IDistributedCache _distributedCache;
        private readonly CultureInfo cultureInfo;
        #endregion

        #region Constructor
        public RedisManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            cultureInfo = new("en-US");
        }
        #endregion

        #region Methods
        public Task ConnectServerAsync()
        {
            throw new NotImplementedException();
        }
        public void Increment(string cacheKey, int increment = 1)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllKeys()
        {
            throw new NotImplementedException();
        }

        public Task IncrementAsync(string cacheKey, int increment = 1)
        {
            throw new NotImplementedException();
        }

        public Task RemoveKeysBySearchKeyAsync(string searchKey)
        {
            throw new NotImplementedException();
        }

        public IResult TryGetValue<TModel>(string cacheKey, out TModel value)
        {
            value = default;

            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            string cachedValue = _distributedCache.GetString(cacheKey.ToLower(cultureInfo));
            if (string.IsNullOrWhiteSpace(cachedValue))
                return new ErrorResult();

            value = cachedValue.AsModel<TModel>();
            return new SuccessResult();
        }

        public IDataResult<TModel> Get<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            string cachedValue = _distributedCache.GetString(cacheKey.ToLower(cultureInfo));
            if (string.IsNullOrWhiteSpace(cachedValue))
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(cachedValue.AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> GetAsync<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            string cachedValue = await _distributedCache.GetStringAsync(cacheKey.ToLower(cultureInfo));
            if (string.IsNullOrWhiteSpace(cachedValue))
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(cachedValue.AsModel<TModel>());
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await _distributedCache.SetStringAsync(cacheKey.ToLower(cultureInfo), value.ToJsonString());
        }

        public void Set<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpiration.ToInt()),
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration.ToInt())
            };

            _distributedCache.SetString(cacheKey.ToLower(cultureInfo), value.ToJsonString(), distributedCacheEntryOptions);
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpiration.ToInt()),
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration.ToInt())
            };

            await _distributedCache.SetStringAsync(cacheKey.ToLower(cultureInfo), value.ToJsonString(), distributedCacheEntryOptions);
        }

        public void Remove(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            _distributedCache.Remove(cacheKey.ToLower(cultureInfo));
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await _distributedCache.RemoveAsync(cacheKey.ToLower(cultureInfo));
        }

        public Task ClearAppCacheAsync()
        {
            throw new NotImplementedException();
        }

        public void RemoveKeysBySearchKey(string searchKey, KeySearchType keySearchType)
        {
            throw new NotImplementedException();
        }

        public Task RemoveKeysBySearchKeyAsync(string searchKey, KeySearchType keySearchType)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
