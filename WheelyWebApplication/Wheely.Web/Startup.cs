using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wheely.Core.DependencyResolvers;
using Wheely.Service.Redis;
using Wheely.Service.Routes;
using Wheely.Web.Infrastructure.IOC;
using Wheely.Web.Infrastructure.Middlewares;

namespace Wheely.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultServices();
            services.AddDbContexts();
            services.AddRedis();
            services.AddScopedServices();
            services.AddSingletonServices();
            services.AddSettings();
            ServiceTool.Create(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRouteService routeService, IRedisService redisService)
        {
            MiddlewareExtensions.PrepareApplicationsRequirements();

            //app.UseSecurityHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/InternalServerError");
                //app.UseStatusCodePagesWithReExecute("/Error/Handle", "?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();

            app.UseSmidgeConfig();
            app.UseEndpointConfig();
        }
    }
}
