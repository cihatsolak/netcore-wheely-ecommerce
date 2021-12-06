namespace Wheely.Core.Web.Settings.RedisServerSettings
{
    public interface IRedisServerSettings : ISettings
    {
        int Database { get; set; }
        string ConnectionString { get; set; }
        bool AbortOnConnectFail { get; set; }
        int AsyncTimeOutMilliSecond { get; set; }
        int ConnectTimeOutMilliSecond { get; set; }
        string SearchKey { get; set; }
    }
}
