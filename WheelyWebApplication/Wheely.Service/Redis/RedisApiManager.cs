using StackExchange.Redis;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Wheely.Core.Enums;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Core.Utilities;
using Wheely.Core.Web.Settings.RedisServerSettings;
using Wheely.Service.Consul;

namespace Wheely.Service.Redis
{
    public sealed class RedisApiManager : IRedisService
    {
        #region Fields
        private ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly CultureInfo cultureInfo;
        private readonly RedisServerSettings _redisServerSettings;
        #endregion

        #region Constructor
        public RedisApiManager(IConsulService consulService)
        {
            var redisServerSettings = consulService.Get<RedisServerSettings>(nameof(RedisServerSettings));
            if (!redisServerSettings.Succeeded)
                throw new Exception();

            _database = _connectionMultiplexer.GetDatabase(redisServerSettings.Data.Database);
            cultureInfo = new("en-US");
        }
        #endregion

        #region Middleware
        public async void ConnectServerAsync()
        {
            ConfigurationOptions configurationOptions = new()
            {
                EndPoints = { _redisServerSettings.ConnectionString },
                AbortOnConnectFail = _redisServerSettings.AbortOnConnectFail,
                AsyncTimeout = _redisServerSettings.AsyncTimeOutMilliSecond,
                ConnectTimeout = _redisServerSettings.ConnectTimeOutMilliSecond
            };

            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(configurationOptions);
        }
        #endregion

        public IResult TryGetValue<TModel>(string cacheKey, out TModel value)
        {
            value = default;

            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

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

            _database.StringSet(cacheKey.ToLower(cultureInfo), value.ToJsonString());
        }

        public async Task SetAsync<TModel>(string cacheKey, TModel value, SlidingExpiration slidingExpiration = SlidingExpiration.ThreeMinute, AbsoluteExpiration absoluteExpiration = AbsoluteExpiration.TwentyMinutes)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentNullException(nameof(cacheKey));

            await _database.StringSetAsync(cacheKey.ToLower(cultureInfo), value.ToJsonString());
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
    }
}
