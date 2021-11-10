using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
            services.AddScopedServices().AddSingletonServices();
            services.AddSettings();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseSmidgeConfig();
            app.UseEndpointConfig();
        }
    }
}
