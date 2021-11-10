using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Wheely.Data.Concrete.Extensions;

namespace Wheely.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.Sources.Clear();
                var host = hostingContext.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{host.EnvironmentName}.json",
                                     optional: true, reloadOnChange: true);
            })
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.ConfigureKestrel(serverOptions =>
               {
                   //Response üzerinden Server Header bilgisi kaldýrýlýr
                   serverOptions.AddServerHeader = false;
               });

               webBuilder.UseStartup<Startup>();
           });
    }
}
