using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;
using System.Net.Mime;
using Wheely.Core.Constants;
using Wheely.Core.DependencyResolvers;
using Wheely.Core.Web.Settings;

namespace Wheely.Web.Infrastructure.IOC
{
    internal static  class HttpClientDependencyInjection
    {
        /// <summary>
        /// Client provider for http rest api service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="services">type of service collection interface</param>
        /// <returns>type of service collection interface</returns>
        internal static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            var notiflowClientSetting = ServiceTool.ServiceProvider.GetRequiredService<INotiflowClientSetting>();
            var turkuazClientSetting = ServiceTool.ServiceProvider.GetRequiredService<ITurkuazClientSetting>();

            services.AddHttpClient(HttpClientNameConstants.Turkuaz, configureClient =>
            {
                configureClient.BaseAddress = new Uri(turkuazClientSetting.BaseAddress);
                //configureClient.DefaultRequestHeaders.Add("PrivateKey", "asdasd");
                configureClient.Timeout = TimeSpan.FromMinutes(turkuazClientSetting.TimeOut);
                configureClient.DefaultRequestHeaders.ExpectContinue = turkuazClientSetting.ExpectContinue;
                configureClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            services.AddHttpClient(HttpClientNameConstants.Notiflow, configureClient =>
            {
                configureClient.BaseAddress = new Uri(notiflowClientSetting.BaseAddress);
                configureClient.Timeout = TimeSpan.FromMinutes(notiflowClientSetting.TimeOut);
                configureClient.DefaultRequestHeaders.ExpectContinue = notiflowClientSetting.ExpectContinue;
                configureClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            return services;
        }
    }
}
