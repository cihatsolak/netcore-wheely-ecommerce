using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Smidge;
using System.Net;
using Wheely.Core.Utilities;
using Wheely.Core.Web.Constants;
using Wheely.Web.Infrastructure.Middlewares.Partials;
using Wheely.Web.Infrastructure.Routes;

namespace Wheely.Web.Infrastructure.Middlewares
{
    /// <summary>
    /// Middleware Extension Methods
    /// </summary>
    internal static class MiddlewareExtensions
    {
        /// <summary>
        /// Security header middleware
        /// </summary>
        /// <param name="app">type of application builder interface</param>
        /// <returns>type of application builder interface</returns>
        internal static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SecurityHeadersMiddleware>();
        }

        /// <summary>
        /// Static files configuration with smidge 
        /// </summary>
        /// <param name="app">type of application builder interface</param>
        /// <returns>type of application builder interface</returns>
        internal static IApplicationBuilder UseSmidgeConfig(this IApplicationBuilder app)
        {
            app.UseSmidge(bundle =>
            {
                bundle.CreateCss(BundleNames.BaseCssFile, SmidgeFile.CSSFiles);
                bundle.CreateJs(BundleNames.BaseJSFile, SmidgeFile.JavaScriptFiles);
            });

            return app;
        }

        /// <summary>
        /// Route value transformer
        /// </summary>
        /// <param name="app">type of application builder interface</param>
        /// <returns>type of application builder interface</returns>
        internal static IApplicationBuilder UseEndpointConfig(this IApplicationBuilder app)
        {
            app.UseEndpoints(configure =>
            {
                configure.MapDynamicControllerRoute<RouteValueTransformer>("{**slug}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            return app;
        }

        internal static IApplicationBuilder UseHttpStatusCodeHandler(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke();

                if (!context.Request.Path.Value.Equals("/") && !context.Request.Path.Value.Contains('.'))
                {
                    if (context.Response.StatusCode == HttpStatusCode.NotFound.ToInt())
                    {
                        context.Request.Path = new PathString("/Error/StatusCode404");
                        await next();
                    }
                    //else if (context.Response.StatusCode == HttpStatusCode.InternalServerError.ToInt())
                    //{
                    //    context.Request.Path = "/Application/Error";
                    //    await _next(context);
                    //}
                }
            });

            return app;
        }
    }
}
