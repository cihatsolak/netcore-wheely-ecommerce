using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Smidge;
using System;
using System.Reflection;
using Wheely.Core.DependencyResolvers;
using Wheely.Core.Web.Settings.GoogleReCaptchaSettings;
using Wheely.Core.Web.Settings.RedisServerSettings;
using Wheely.Core.Web.Settings.SmidgeSettings;
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
            services.AddDbContext<WheelDbContext>(contextOptions =>
            {
                contextOptions.UseLazyLoadingProxies(true);
                contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
                contextOptions.UseNpgsql(ServiceTool.Configuration.GetConnectionString(nameof(WheelDbContext)), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(WheelDbContext).Assembly.FullName);
                    sqlOptions.CommandTimeout(Convert.ToInt16(TimeSpan.FromMinutes(1).TotalSeconds));
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
            services.AddSmidge(ServiceTool.Configuration.GetSection(nameof(SmidgeSettings)));
            services.Configure<GoogleReCaptchaSetting>(ServiceTool.Configuration.GetSection(nameof(GoogleReCaptchaSetting)));
            services.Configure<RedisServerSettings>(ServiceTool.Configuration.GetSection(nameof(RedisServerSettings)));
            #endregion

            #region Singleton Service Dependencies
            services.TryAddSingleton<IGoogleReCaptchaSetting>(provider => provider.GetRequiredService<IOptions<GoogleReCaptchaSetting>>().Value);
            services.TryAddSingleton<IRedisServerSettings>(provider => provider.GetRequiredService<IOptions<RedisServerSettings>>().Value);
            #endregion

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
                options.Configuration = ServiceTool.Configuration.GetValue<string>($"{nameof(RedisServerSettings)}:ConnectionString");
            });

            return services;
        }
    }
}
