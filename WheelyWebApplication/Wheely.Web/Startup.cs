using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Wheely.Core.DependencyResolvers;
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
            services.AddSettings();
            services.AddDbContexts();
            services.AddRedis();
            services.AddScopedServices();
            services.AddSingletonServices();
            services.AddModelFactoryServices();
            services.AddHttpClients();
            ServiceTool.Create(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.LearnRoutes();

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
