using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Smidge;
using System;
using System.Reflection;
using Wheely.Core.DependencyResolvers;
using Wheely.Core.Web.Settings.RedisServerSettings;
using Wheely.Core.Web.Settings.SmidgeSettings;
using Wheely.Data.Concrete.Contexts;
using Wheely.Web.Infrastructure.Routes;

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
            services.AddSingleton<RouteValueTransformer>();

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
            services.AddSmidge(ServiceTool.Configuration.GetSection(nameof(SmidgeSettings)));

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
