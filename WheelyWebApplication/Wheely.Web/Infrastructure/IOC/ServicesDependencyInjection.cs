using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Repositories.EntityFrameworkCore;
using Wheely.Service.Categories;
using Wheely.Service.Consul;
using Wheely.Service.Cookies;
using Wheely.Service.HttpRequest;
using Wheely.Service.Redis;
using Wheely.Service.Routes;
using Wheely.Service.Wheels;
using Wheely.Web.Factories.ShopFactories;
using Wheely.Web.Infrastructure.Routes;

namespace Wheely.Web.Infrastructure.IOC
{
    internal static class ServicesDependencyInjection
    {
        /// <summary>
        /// Dependency injection for services layer scoped services type
        /// </summary>
        /// <param name="services">type of IServiceCollection</param>
        /// <returns>type of IServiceCollection</returns>
        internal static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            #region Repositories
            services.TryAddScoped(typeof(IEntityRepository<>), typeof(EfEntityRepositoryBase<>));
            services.TryAddScoped<IWheelRepository, WheelRepository>();
            services.TryAddScoped<ICategoryRepository, CategoryRepository>();
            services.TryAddScoped<IRouteRepository, RouteRepository>();
            #endregion

            #region Services
            services.TryAddScoped<IWheelService, WheelManager>();
            services.TryAddScoped<ICategoryService, CategoryManager>();
            services.TryAddScoped<IRouteService, RouteManager>();
            #endregion

            #region Model Factories
            services.TryAddScoped<IShopModelFactory, ShopModelFactory>();
            #endregion

            return services;
        }

        /// <summary>
        /// Dependency injection for services layer singleton services type
        /// </summary>
        /// <param name="services">type of IServiceCollection</param>
        /// <returns>type of IServiceCollection</returns>
        internal static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {
            services.TryAddSingleton<IRestApiService, RestApiManager>();
            //services.TryAddSingleton<IRedisService, RedisManager>();
            services.TryAddSingleton<IRedisService, RedisApiManager>();
            services.TryAddSingleton<ICookieService, CookieManager>();
            services.TryAddSingleton<RouteValueTransformer>();
            services.TryAddSingleton<IConsulService, ConsulManager>();
            return services;
        }
    }
}
