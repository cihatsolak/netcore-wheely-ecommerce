using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Web.Settings;

namespace Wheely.Service.Consul
{
    public partial interface IConsulService
    {
        /// <summary>
        /// Getting data from json file 
        /// </summary>
        /// <typeparam name="TSettings">type to convert</typeparam>
        /// <param name="key">key name</param>
        /// <returns></returns>
        IDataResult<TSettings> Get<TSettings>(string key) where TSettings : ISettings;
    }
}
