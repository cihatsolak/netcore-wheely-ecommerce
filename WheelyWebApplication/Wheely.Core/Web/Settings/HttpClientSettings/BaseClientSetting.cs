namespace Wheely.Core.Web.Settings.HttpClientSettings
{
    public abstract class BaseClientSetting
    {
        public string BaseAddress { get; set; }
        public int TimeOut { get; set; }
        public bool ExpectContinue { get; set; }
    }
}
