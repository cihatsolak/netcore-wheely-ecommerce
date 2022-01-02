using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Smidge;
using StackExchange.Redis;
using System;
using System.Reflection;
using Wheely.Core.DependencyResolvers;
using Wheely.Core.Web.Settings;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Web.Infrastructure.IOC
{
    internal static class MicrosoftIOCExtension
    {
        /// <summary>
        /// Add default services
        /// </summary>
        /// <param name="services">type of service collection interface</param>
        /// <returns>type of service collection interface</returns>
        internal static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            ServiceTool.Create(services);
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddFluentValidation(configurationExpression =>
                {
                    configurationExpression.RegisterValidatorsFromAssemblyContaining<Startup>();
                })
                .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                );

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpClient();

            return services;
        }

        /// <summary>
        /// Add database context
        /// </summary>
        /// <param name="services">type of service collection interface</param>
        /// <returns>type of service collection interface</returns>
        internal static IServiceCollection AddDbContexts(this IServiceCollection services)
        {
            IHostEnvironment hostEnvironment = ServiceTool.ServiceProvider.GetRequiredService<IHostEnvironment>();

            services.AddDbContextPool<WheelDbContext>(contextOptions =>
            {
                contextOptions.UseLazyLoadingProxies(true);
                contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
                contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
                contextOptions.LogTo(Log.Logger.Warning, LogLevel.Warning);
                contextOptions.EnableSensitiveDataLogging(hostEnvironment.IsDevelopment());
                contextOptions.UseLoggerFactory(LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                }));

                contextOptions.UseNpgsql(ServiceTool.Configuration.GetConnectionString(nameof(WheelDbContext)), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(WheelDbContext).Assembly.FullName);
                    sqlOptions.CommandTimeout(Convert.ToInt16(TimeSpan.FromSeconds(60).TotalSeconds));
                    sqlOptions.EnableRetryOnFailure();
                });
            });

            return services;
        }

        /// <summary>
        /// Add settings - const data in config file
        /// </summary>
        /// <param name="services">type of service collection interface</param>
        /// <returns>type of service collection interface</returns>
        internal static IServiceCollection AddSettings(this IServiceCollection services)
        {
            #region Configuration Dependencies
            services.AddSmidge(ServiceTool.Configuration.GetSection(nameof(SmidgeSetting)));
            services.Configure<GoogleReCaptchaSetting>(ServiceTool.Configuration.GetSection(nameof(GoogleReCaptchaSetting)));
            services.Configure<RedisServerSetting>(ServiceTool.Configuration.GetSection(nameof(RedisServerSetting)));
            services.Configure<NotiflowClientSetting>(ServiceTool.Configuration.GetSection(nameof(NotiflowClientSetting)));
            services.Configure<TurkuazClientSetting>(ServiceTool.Configuration.GetSection(nameof(TurkuazClientSetting)));
            #endregion

            #region Singleton Service Dependencies
            services.TryAddSingleton<IGoogleReCaptchaSetting>(provider => provider.GetRequiredService<IOptions<GoogleReCaptchaSetting>>().Value);
            services.TryAddSingleton<IRedisServerSetting>(provider => provider.GetRequiredService<IOptions<RedisServerSetting>>().Value);
            services.TryAddSingleton<INotiflowClientSetting>(provider => provider.GetRequiredService<IOptions<NotiflowClientSetting>>().Value);
            services.TryAddSingleton<ITurkuazClientSetting>(provider => provider.GetRequiredService<IOptions<TurkuazClientSetting>>().Value);
            #endregion
            ServiceTool.Create(services);
            return services;
        }

        /// <summary>
        /// Add redis configuration
        /// </summary>
        /// <param name="services">type of service collection interface</param>
        /// <returns>type of service collection interface</returns>
        internal static IServiceCollection AddRedis(this IServiceCollection services)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = ServiceTool.Configuration.GetValue<string>($"{nameof(RedisServerSetting)}:ConnectionString");
            });

            return services;
        }

        /// <summary>
        /// Dependency injection for data protection
        /// </summary>
        /// <param name="services">type of IServiceCollection</param>
        /// <returns>type of IServiceCollection</returns>
        internal static IServiceCollection AddProtection(this IServiceCollection services)
        {
            var webHostEnvironment = ServiceTool.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            var connectionMultiplexer = ServiceTool.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();

            var applicationName = string.Concat(webHostEnvironment.ApplicationName, ".", webHostEnvironment.EnvironmentName).ToLower();
            var redisKey = string.Concat(webHostEnvironment.ApplicationName, ".", webHostEnvironment.EnvironmentName, ".", "dataprotection.keys").ToLower();

            services.AddDataProtection()
                .SetApplicationName(applicationName)
                .PersistKeysToStackExchangeRedis(connectionMultiplexer, redisKey);

            return services;
        }
    }
}
