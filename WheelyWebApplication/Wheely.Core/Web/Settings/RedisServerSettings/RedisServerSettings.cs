namespace Wheely.Core.Web.Settings.RedisServerSettings
{
    public sealed class RedisServerSettings : IRedisServerSettings
    {
        public int Database { get; set; }
        public string ConnectionString { get; set; }
        public bool AbortOnConnectFail { get; set; }
        public int AsyncTimeOutMilliSecond { get; set; }
        public int ConnectTimeOutMilliSecond { get; set; }
    }
}
