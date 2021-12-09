namespace Wheely.Core.Web.Settings
{
    public interface IRedisServerSetting : ISettings
    {
        int Database { get; set; }
        string ConnectionString { get; set; }
        bool AbortOnConnectFail { get; set; }
        int AsyncTimeOutMilliSecond { get; set; }
        int ConnectTimeOutMilliSecond { get; set; }
        string SearchKey { get; set; }
    }

    public sealed class RedisServerSetting : IRedisServerSetting
    {
        public int Database { get; set; }
        public string ConnectionString { get; set; }
        public bool AbortOnConnectFail { get; set; }
        public int AsyncTimeOutMilliSecond { get; set; }
        public int ConnectTimeOutMilliSecond { get; set; }
        public string SearchKey { get; set; }
    }
}
