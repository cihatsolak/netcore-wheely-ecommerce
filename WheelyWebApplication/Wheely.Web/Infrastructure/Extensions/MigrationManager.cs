using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Wheely.Data.Concrete.Contexts;
using Wheely.Data.Concrete.Seeds;

namespace Wheely.Web.Infrastructure.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            try
            {
                WheelDbContext wheelDbContext = serviceScope.ServiceProvider.GetRequiredService<WheelDbContext>();

                if (wheelDbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    wheelDbContext.Database.Migrate();
                }

                WheelDbContextSeed.SeedAsync(wheelDbContext, loggerFactory).Wait();
            }
            catch (Exception exception)
            {
                var logger = loggerFactory.CreateLogger(nameof(MigrationManager));
                logger.LogError(exception, "An error occured seeding the DB");
            }

            return host;
        }
    }
}
