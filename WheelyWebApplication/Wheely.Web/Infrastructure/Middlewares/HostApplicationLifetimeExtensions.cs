using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Wheely.Core.DependencyResolvers;
using Wheely.Service.Redis;
using Wheely.Service.Routes;

namespace Wheely.Web.Infrastructure.Middlewares
{
    /// <summary>
    /// Application requirements extensions
    /// </summary>
    internal static class HostApplicationLifetimeExtensions
    {
        #region Fields
        private static IRedisService RedisService { get; set; }
        private static IRouteService RouteService { get; set; }
        #endregion

        /// <summary>
        /// Learn routes
        /// </summary>
        /// <param name="app">type of application builder interface</param>
        /// <returns>type of application builder interface</returns>
        internal static IApplicationBuilder LearnRoutes(this IApplicationBuilder app)
        {
            RedisService = ServiceTool.ServiceProvider.GetService<IRedisService>();
            RouteService = ServiceTool.ServiceProvider.GetService<IRouteService>();

            if (RedisService is null)
            {
                //logger.LogCritical("Redis bağlantısı kurulamadı @redisService", redisService);
                throw new ArgumentNullException(nameof(RedisService));
            }


            if (RouteService is null)
            {
                //logger.LogCritical("Redis bağlantısı kurulamadı @redisService", redisService);
                throw new ArgumentNullException(nameof(RouteService));
            }

            var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
            hostApplicationLifetime.ApplicationStopped.Register(OnStopped);

            return app;
        }

        #region Host Application Life Time
        private static void OnStarted()
        {
            RedisService.ConnectServerAsync().Wait();
            RouteService.GetRoutesAsync().Wait();
        }

        private static void OnStopping()
        {
            RedisService.ClearAppCacheAsync();
        }

        private static void OnStopped()
        {
        }
        #endregion
    }
}
