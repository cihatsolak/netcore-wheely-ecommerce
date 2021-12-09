using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            try
            {
                RedisService = ServiceTool.ServiceProvider.GetRequiredService<IRedisService>();
                RouteService = ServiceTool.ServiceProvider.GetRequiredService<IRouteService>();

                var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

                hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
                hostApplicationLifetime.ApplicationStopping.Register(OnStopping);
                hostApplicationLifetime.ApplicationStopped.Register(OnStopped);
            }
            catch (Exception ex)
            {
                //_logger.log();
            }
            finally
            {
                
            }

            return app;
        }

        #region Host Application Life Time
        private static void OnStarted()
        {
            RedisService.ConnectServer().Wait();
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
