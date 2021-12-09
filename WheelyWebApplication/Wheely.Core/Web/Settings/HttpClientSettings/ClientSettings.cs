namespace Wheely.Core.Web.Settings.HttpClientSettings
{
    public interface INotiflowClientSetting
    {
    }
    public interface ITurkuazClientSetting
    {
    }

    public sealed class NotiflowClientSetting : BaseClientSetting, INotiflowClientSetting
    {
    }

    public sealed class TurkuazClientSetting : BaseClientSetting, ITurkuazClientSetting
    {
    }
}
