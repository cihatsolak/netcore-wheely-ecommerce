using Microsoft.Extensions.DependencyInjection;
using Wheely.Data.Abstract;
using Wheely.Data.Concrete.EntityFrameworkCore;
using Wheely.Service.Categories;

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
            services.AddScoped(typeof(IEntityRepository<>), typeof(EfEntityRepositoryBase<>));
            services.AddScoped<ICategoryService, CategoryManager>();

            return services;
        }

        /// <summary>
        /// Dependency injection for services layer singleton services type
        /// </summary>
        /// <param name="services">type of IServiceCollection</param>
        /// <returns>type of IServiceCollection</returns>
        internal static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
