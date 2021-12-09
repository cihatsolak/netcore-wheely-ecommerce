namespace Wheely.Core.Web.Settings.HttpClientSettings
{
    public interface IHttpClientSettings : ISettings
    {
    }

    public sealed class HttpClientSettings : IHttpClientSettings
    {
        public TurkuazClientSetting TurkuazClientSetting { get; set; }
        public NotiflowClientSetting NotiflowClientSetting { get; set; }
    }
}
