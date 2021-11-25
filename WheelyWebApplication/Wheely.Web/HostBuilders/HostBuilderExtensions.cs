using Consul;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Winton.Extensions.Configuration.Consul;

namespace Wheely.Web.HostBuilders
{
    internal static class HostBuilderExtensions
    {
        //internal static IHostBuilder AddAppConfiguration(this IHostBuilder hostBuilder)
        //{
        //    hostBuilder.ConfigureAppConfiguration((hostingContext, config) =>
        //    {
        //        config.Sources.Clear();
        //        var host = hostingContext.HostingEnvironment;
        //        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //              .AddJsonFile($"appsettings.{host.EnvironmentName}.json",
        //                             optional: true, reloadOnChange: true);
        //    });

        //    return hostBuilder;
        //}

        /// <summary>
        /// Add host configuration
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <returns>type of host builder interface</returns>
        internal static IHostBuilder AddHostConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureHostConfiguration(configurationBuilder =>
            {
                configurationBuilder.AddJsonFile("appsettings.json", false, true);
                configurationBuilder.AddJsonFile("appsettings.Development.json", false, true);
            });

            return hostBuilder;
        }

        /// <summary>
        /// Add app configuration
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <returns>type of host builder interface</returns>
        internal static IHostBuilder AddAppConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
            {
                string consulHost = hostBuilderContext.Configuration["ConsulHost"];
                string applicationName = hostBuilderContext.HostingEnvironment.ApplicationName;
                string environmentName = hostBuilderContext.HostingEnvironment.EnvironmentName;

                void ConsulConfig(ConsulClientConfiguration configuration)
                {
                    configuration.Address = new Uri(consulHost);
                }

                configurationBuilder.AddConsul($"{applicationName}/appsettings.json", source =>
                {
                    source.ReloadOnChange = true;
                    source.ConsulConfigurationOptions = ConsulConfig;
                });

                configurationBuilder.AddConsul($"{applicationName}/appsettings.{environmentName}.json", source =>
                {
                    source.Optional = true;
                    source.ConsulConfigurationOptions = ConsulConfig;
                });
            });

            return hostBuilder;
        }

        /// <summary>
        /// Add web host defaults
        /// </summary>
        /// <param name="hostBuilder">host builder</param>
        /// <returns>type of host builder interface</returns>
        internal static IHostBuilder AddWebHostDefaults(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.ConfigureKestrel(serverOptions =>
                 {
                     serverOptions.AddServerHeader = false; //Response üzerinden Server Header bilgisi kaldırılır
                 });

                 webBuilder.UseStartup<Startup>();
             });

            return hostBuilder;
        }

    }
}
