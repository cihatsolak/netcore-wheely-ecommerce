using Microsoft.Extensions.DependencyInjection;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Repositories.EntityFrameworkCore;
using Wheely.Service.Categories;
using Wheely.Service.HttpRequest;
using Wheely.Service.Redis;
using Wheely.Service.Routes;
using Wheely.Service.Wheels;
using Wheely.Web.Factories.ShopFactories;

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
            services.AddScoped(typeof(IEntityRepository<>), typeof(EfEntityRepositoryBase<>));
            services.AddScoped<IWheelRepository, WheelRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IRouteRepository, RouteRepository>();
            #endregion

            #region Services
            services.AddScoped<IWheelService, WheelManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IRouteService, RouteManager>();
            #endregion

            #region Model Factories
            services.AddScoped<IShopModelFactory, ShopModelFactory>();
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
            services.AddSingleton<IRestApiService, RestApiManager>();
            services.AddSingleton<IRedisService, RedisManager>();

            return services;
        }
    }
}
