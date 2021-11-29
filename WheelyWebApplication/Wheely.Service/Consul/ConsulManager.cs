using Microsoft.Extensions.Configuration;
using System;
using Wheely.Core.Web.Settings;

namespace Wheely.Service.Consul
{
    public sealed class ConsulManager : IConsulService
    {
        private readonly IConfiguration _configuration;

        public ConsulManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TSettings Get<TSettings>(string key) where TSettings : ISettings
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            TSettings settingsModel = _configuration.GetSection(key).Get<TSettings>();
            if (settingsModel is null)
                throw new ArgumentNullException(nameof(settingsModel));

            return settingsModel;
        }
    }
}
