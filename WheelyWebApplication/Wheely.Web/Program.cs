using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Wheely.Data.Concrete.Extensions;
using Wheely.Web.HostBuilders;

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
            .AddHostConfiguration()
            .AddAppConfiguration()
            .AddWebHostDefaults();
    }
}
