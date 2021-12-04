using Microsoft.Extensions.Configuration;
using System;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Core.Web.Settings;

namespace Wheely.Service.Consul
{
    public sealed class ConsulManager : IConsulService
    {
        #region Fields
        private readonly IConfiguration _configuration;
        #endregion

        #region Constructor
        public ConsulManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public IDataResult<TSettings> Get<TSettings>(string key) where TSettings : ISettings
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            TSettings settingsModel = _configuration.GetSection(key).Get<TSettings>();
            if (settingsModel is null)
                return new ErrorDataResult<TSettings>();

            return new SuccessDataResult<TSettings>(settingsModel);
        }
        #endregion
    }
}
