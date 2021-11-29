using Wheely.Core.Web.Settings;

namespace Wheely.Service.Consul
{
    public partial interface IConsulService
    {
        TSettings Get<TSettings>(string key) where TSettings : ISettings;
    }
}
