using StackExchange.Redis;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Wheely.Core.Enums;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Core.Utilities;
using Wheely.Core.Web.Settings.RedisServerSettings;

namespace Wheely.Service.Redis
{
    public partial class RedisApiManager : IRedisService
    {
        #region Fields
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private readonly CultureInfo cultureInfo;
        private readonly IRedisServerSettings _redisServerSettings;
        #endregion

        #region Constructor
        public RedisApiManager(IRedisServerSettings redisServerSettings)
        {
            _redisServerSettings = redisServerSettings;
            cultureInfo = new("en-US");
        }
        #endregion

        public void Increment(string cacheKey, int increment = 1)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            _database.StringIncrement(cacheKey.ToLower(cultureInfo), increment);
        }

        public async Task IncrementAsync(string cacheKey, int increment = 1)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await _database.StringIncrementAsync(cacheKey.ToLower(cultureInfo), increment);
        }

        public IResult TryGetValue<TModel>(string cacheKey, out TModel value)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            value = default;
            var redisValue = _database.StringGet(cacheKey.ToLower(cultureInfo));
            if (!redisValue.HasValue)
                return new ErrorResult();

            value = ((string)redisValue).AsModel<TModel>();
            return new SuccessResult();
        }

        public IDataResult<TModel> Get<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            var redisValue = _database.StringGet(cacheKey.ToLower(cultureInfo));
            if (!redisValue.HasValue)
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(((string)redisValue).AsModel<TModel>());
        }

        public async Task<IDataResult<TModel>> GetAsync<TModel>(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            var redisValue = await _database.StringGetAsync(cacheKey.ToLower(cultureInfo));
            if (!redisValue.HasValue)
                return new ErrorDataResult<TModel>();

            return new SuccessDataResult<TModel>(redisValue.ToString().AsModel<TModel>());
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await _database.StringSetAsync(cacheKey.ToLower(cultureInfo), value.ToJsonString());
        }

        public void Set<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            _database.StringSet(cacheKey, value.ToJsonString());
            _database.KeyExpire(cacheKey, DateTime.Now.AddMinutes(absoluteExpiration.ToInt()));
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            await _database.StringSetAsync(cacheKey.ToLower(cultureInfo), value.ToJsonString());
            await _database.KeyExpireAsync(cacheKey, DateTime.Now.AddMinutes(absoluteExpiration.ToInt()));
        }

        public void Remove(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            if (_database.KeyExists(cacheKey))
            {
                _database.KeyDelete(cacheKey);
            }
        }

        public async Task RemoveAsync(string cacheKey)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            cacheKey = cacheKey.ToLower(cultureInfo);

            if (await _database.KeyExistsAsync(cacheKey))
            {
                await _database.KeyDeleteAsync(cacheKey);
            }
        }

        public void RemoveKeysBySearchKey(string searchKey, KeySearchType keySearchType)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
                throw new ArgumentNullException(nameof(searchKey));

            IServer server = _connectionMultiplexer.GetServer(_redisServerSettings.ConnectionString);

            searchKey = keySearchType switch
            {
                KeySearchType.EndWith => $"*{searchKey}",
                KeySearchType.StartWith => $"{searchKey}*",
                KeySearchType.Include => $"*{searchKey}*",
                _ => $"*{searchKey}*",
            };

            var redisKeys = server.Keys(_redisServerSettings.Database, searchKey).ToArray();
            _database.KeyDelete(redisKeys);
        }

        public async Task RemoveKeysBySearchKeyAsync(string searchKey, KeySearchType keySearchType)
        {
            if (string.IsNullOrWhiteSpace(searchKey))
                throw new ArgumentNullException(nameof(searchKey));

            IServer server = _connectionMultiplexer.GetServer(_redisServerSettings.ConnectionString);

            searchKey = keySearchType switch
            {
                KeySearchType.EndWith => $"*{searchKey}",
                KeySearchType.StartWith => $"{searchKey}*",
                KeySearchType.Include => $"*{searchKey}*",
                _ => $"*{searchKey}*",
            };

            var redisKeys = server.Keys(_redisServerSettings.Database, searchKey).ToArray();
            await _database.KeyDeleteAsync(redisKeys);
        }
    }
}
