using Microsoft.Extensions.Caching.Distributed;
using System;
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
        #endregion

        #region Constructor
        public RedisManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        #endregion

        #region Methods
        public IResult TryGetValue<TModel>(string cacheKey, out TModel value)
        {
            value = default;

            if (string.IsNullOrWhiteSpace(cacheKey)) return new ErrorResult();

            string cachedValue = _distributedCache.GetString(cacheKey.ToLower());
            if (string.IsNullOrWhiteSpace(cachedValue))
                return new ErrorResult(default);

            value = cachedValue.AsModel<TModel>();
            return new ErrorResult();
        }

        public IDataResult<TModel> Get<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return new ErrorDataResult<TModel>();

            string cachedValue = _distributedCache.GetString(cacheKey.ToLower());
            if (string.IsNullOrWhiteSpace(cachedValue))
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(cachedValue.AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> GetAsync<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return new ErrorDataResult<TModel>();

            string cachedValue = await _distributedCache.GetStringAsync(cacheKey.ToLower());
            if (string.IsNullOrWhiteSpace(cachedValue))
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(cachedValue.AsModel<TModel>());
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return;

            await _distributedCache.SetStringAsync(cacheKey.ToLower(), value.ToJsonString());
        }

        public IResult Set<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return new ErrorResult();

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpiration.ToInt()),
                SlidingExpiration = TimeSpan.FromMinutes(slidingExpiration.ToInt())
            };

            _distributedCache.SetString(cacheKey.ToLower(), value.ToJsonString(), distributedCacheEntryOptions);
            return new SuccessResult();
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

        public IResult Remove(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return new ErrorResult();

            _distributedCache.Remove(cacheKey.ToLower());
            return new SuccessResult();
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey)) return;

            await _distributedCache.RemoveAsync(cacheKey.ToLower());
        }
        #endregion
    }
}
