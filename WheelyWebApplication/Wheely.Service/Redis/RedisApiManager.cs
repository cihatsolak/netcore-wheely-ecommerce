using StackExchange.Redis;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Wheely.Core.Enums;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Core.Utilities;
using Wheely.Core.Web.Settings;

namespace Wheely.Service.Redis
{
    public sealed class RedisApiManager : IRedisService 
    {
        #region Fields
        private readonly CultureInfo cultureInfo;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IRedisServerSetting _redisServerSetting;
        #endregion

        #region Constructor
        public RedisApiManager(
            IConnectionMultiplexer connectionMultiplexer, 
            IRedisServerSetting redisServerSetting)
        {
            cultureInfo = new("en-US");
            _connectionMultiplexer = connectionMultiplexer;
            _redisServerSetting = redisServerSetting;
        }
        #endregion

        public void Increment(string cacheKey, int increment = 1)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            Database.StringIncrement(cacheKey.ToLower(cultureInfo), increment);
        }

        public async Task IncrementAsync(string cacheKey, int increment = 1)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await Database.StringIncrementAsync(cacheKey.ToLower(cultureInfo), increment);
        }

        public IResult TryGetValue<TModel>(string cacheKey, out TModel value)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            value = default;
            var redisValue = Database.StringGet(cacheKey.ToLower(cultureInfo));
            if (!redisValue.HasValue)
                return new ErrorResult();

            value = ((string)redisValue).AsModel<TModel>();
            return new SuccessResult();
        }

        public IDataResult<TModel> Get<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            var redisValue = Database.StringGet(cacheKey.ToLower(cultureInfo));
            if (!redisValue.HasValue)
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(((string)redisValue).AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> GetAsync<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            var redisValue = await Database.StringGetAsync(cacheKey.ToLower(cultureInfo));
            if (!redisValue.HasValue)
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(redisValue.ToString().AsModel<TModel>());
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await Database.StringSetAsync(cacheKey.ToLower(cultureInfo), value.ToJsonString());
        }

        public void Set<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            Database.StringSet(cacheKey, value.ToJsonString());
            Database.KeyExpire(cacheKey, DateTime.Now.AddMinutes(absoluteExpiration.ToInt()));
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            await Database.StringSetAsync(cacheKey.ToLower(cultureInfo), value.ToJsonString());
            await Database.KeyExpireAsync(cacheKey, DateTime.Now.AddMinutes(absoluteExpiration.ToInt()));
        }

        public void Remove(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            if (Database.KeyExists(cacheKey))
            {
                Database.KeyDelete(cacheKey);
            }
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            if (await Database.KeyExistsAsync(cacheKey))
            {
                await Database.KeyDeleteAsync(cacheKey);
            }
        }

        public void RemoveKeysBySearchKey(string searchKey, KeySearchType keySearchType)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
                throw new ArgumentNullException(nameof(searchKey));

            IServer server = _connectionMultiplexer.GetServer(_redisServerSetting.ConnectionString);

            searchKey = keySearchType switch
            {
                KeySearchType.EndWith => $"*{searchKey}",
                KeySearchType.StartWith => $"{searchKey}*",
                KeySearchType.Include => $"*{searchKey}*",
                _ => $"*{searchKey}*",
            };

            var redisKeys = server.Keys(_redisServerSetting.Database, searchKey).ToArray();
            Database.KeyDelete(redisKeys);
        }

        public async Task RemoveKeysBySearchKeyAsync(string searchKey, KeySearchType keySearchType)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
                throw new ArgumentNullException(nameof(searchKey));

            IServer server = _connectionMultiplexer.GetServer(_redisServerSetting.ConnectionString);

            searchKey = keySearchType switch
            {
                KeySearchType.EndWith => $"*{searchKey}",
                KeySearchType.StartWith => $"{searchKey}*",
                KeySearchType.Include => $"*{searchKey}*",
                _ => $"*{searchKey}*",
            };

            var redisKeys = server.Keys(_redisServerSetting.Database, searchKey).ToArray();
            await Database.KeyDeleteAsync(redisKeys);
        }

        public async Task ClearAppCacheAsync()
        {
            IServer server = _connectionMultiplexer.GetServer(_redisServerSetting.ConnectionString);
            var redisKeys = server.Keys(_redisServerSetting.Database);
            await Database.KeyDeleteAsync(redisKeys.ToArray());
        }

        private IDatabase Database
        {
            get
            {
                return _connectionMultiplexer.GetDatabase(1);
            }
        }
    }
}
