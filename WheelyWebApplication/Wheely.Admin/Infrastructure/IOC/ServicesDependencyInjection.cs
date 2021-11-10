using Microsoft.Extensions.DependencyInjection;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Repositories.EntityFrameworkCore;

namespace Wheely.Admin.Infrastructure.IOC
{
    public static class ServicesDependencyInjection
    {
        /// <summary>
        /// Dependency injection for services layer scoped services type
        /// </summary>
        /// <param name="services">type of IServiceCollection</param>
        /// <returns>type of IServiceCollection</returns>
        public static IServiceCollection AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IEntityRepository<>), typeof(EfEntityRepositoryBase<>));

            return services;
        }

        /// <summary>
        /// Dependency injection for services layer singleton services type
        /// </summary>
        /// <param name="services">type of IServiceCollection</param>
        /// <returns>type of IServiceCollection</returns>
        public static IServiceCollection AddSingletonServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
