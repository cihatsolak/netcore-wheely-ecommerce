namespace Wheely.Core.Web.Settings
{
    public interface INotiflowClientSetting
    {
        string BaseAddress { get; set; }
        int TimeOut { get; set; }
        bool ExpectContinue { get; set; }
    }
    public interface ITurkuazClientSetting
    {
        string BaseAddress { get; set; }
        int TimeOut { get; set; }
        bool ExpectContinue { get; set; }
    }

    public sealed class NotiflowClientSetting : INotiflowClientSetting
    {
        public string BaseAddress { get; set; }
        public int TimeOut { get; set; }
        public bool ExpectContinue { get; set; }
    }

    public sealed class TurkuazClientSetting : ITurkuazClientSetting
    {
        public string BaseAddress { get; set; }
        public int TimeOut { get; set; }
        public bool ExpectContinue { get; set; }
    }
}
