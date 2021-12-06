using StackExchange.Redis;
using System.Linq;
using System.Threading.Tasks;

namespace Wheely.Service.Redis
{
    public  partial class RedisApiManager : IRedisService
    {
        public async Task ConnectServerAsync()
        {
            ConfigurationOptions configurationOptions = new()
            {
                EndPoints = { _redisServerSettings.ConnectionString },
                AbortOnConnectFail = _redisServerSettings.AbortOnConnectFail,
                AsyncTimeout = _redisServerSettings.AsyncTimeOutMilliSecond,
                ConnectTimeout = _redisServerSettings.ConnectTimeOutMilliSecond
            };

            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(configurationOptions);
            _database = _connectionMultiplexer.GetDatabase(_redisServerSettings.Database);
        }

        public async Task ClearAppCacheAsync()
        {
            IServer server = _connectionMultiplexer.GetServer(_redisServerSettings.ConnectionString);
            var redisKeys = server.Keys(_redisServerSettings.Database, $"*{_redisServerSettings.SearchKey}");
            await _database.KeyDeleteAsync(redisKeys.ToArray());
        }
    }
}
