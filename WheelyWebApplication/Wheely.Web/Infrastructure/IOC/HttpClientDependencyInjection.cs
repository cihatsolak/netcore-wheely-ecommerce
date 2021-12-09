using Microsoft.Extensions.DependencyInjection;
using Wheely.Core.Constants;

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
            //services.AddHttpClient(HttpClientNameConstants.Turkuaz, configureClient =>
            //{
            //    configureClient.BaseAddress = new Uri("https://www.7timer.info");
            //    //configureClient.DefaultRequestHeaders.Add("PrivateKey", "asdasd");
            //    configureClient.Timeout = TimeSpan.FromMinutes(1);
            //    configureClient.DefaultRequestHeaders.ExpectContinue = false;
            //    configureClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            //});

            return services;
        }
    }
}
