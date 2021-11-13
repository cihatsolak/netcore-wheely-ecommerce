﻿using Microsoft.AspNetCore.Builder;
using Smidge;
using Wheely.Web.Infrastructure.Constants;
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
                bundle.CreateJs(BundleNames.JqueryValidation, SmidgeFile.JqueryValidationJavascriptFiles);
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDynamicControllerRoute<RouteValueTransformer>("{**slug}");
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //});

            return app;
        }
    }
}
