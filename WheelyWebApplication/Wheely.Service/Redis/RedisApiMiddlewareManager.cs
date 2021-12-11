using StackExchange.Redis;
using System.Linq;
using System.Threading.Tasks;

namespace Wheely.Service.Redis
{
    public partial class RedisApiManager : IRedisService
    {
        public void ConnectServer()
        {
            ConfigurationOptions configurationOptions = new()
            {
                EndPoints = { _redisServerSetting.ConnectionString },
                AbortOnConnectFail = _redisServerSetting.AbortOnConnectFail,
                AsyncTimeout = _redisServerSetting.AsyncTimeOutMilliSecond,
                ConnectTimeout = _redisServerSetting.ConnectTimeOutMilliSecond
            };

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationOptions);
        }

        public async Task ClearAppCacheAsync()
        {
            IServer server = _connectionMultiplexer.GetServer(_redisServerSetting.ConnectionString);
            var redisKeys = server.Keys(_redisServerSetting.Database);
            await Database.KeyDeleteAsync(redisKeys.ToArray());
        }


        private ConnectionMultiplexer ConnectionMultiplexer
        {
            get
            {
                lock (_multiplexerLock)
                {
                    if (_connectionMultiplexer is null || !_connectionMultiplexer.IsConnected)
                    {
                        _connectionMultiplexer?.Dispose();
                        ConnectServer();
                    }

                    return _connectionMultiplexer;
                }
            }
        }
        private IDatabase Database
        {
            get
            {
                return ConnectionMultiplexer.GetDatabase(1);
            }
        }

        public void Dispose()
        {
            _connectionMultiplexer?.Dispose();
        }
    }
}
