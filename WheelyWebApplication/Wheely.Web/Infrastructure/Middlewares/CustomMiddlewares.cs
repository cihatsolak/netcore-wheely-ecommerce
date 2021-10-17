using Microsoft.AspNetCore.Builder;
using Smidge;

namespace Wheely.Web.Infrastructure.Middlewares
{
    internal static class CustomMiddlewares
    {
        internal static void UseCustomSmidge(this IApplicationBuilder app)
        {
            app.UseSmidge(bundle =>
            {
                bundle.CreateCss("app-styles", "~/assets/css/app.min.css", "~/assets/css/fontawesome.min.css", "~/assets/css/style.css", "~assets/css/theme-color1.css");
                bundle.CreateJs("app-scripts", "~/assets/js/vendor/jquery-1.12.4.min.js", "~/assets/js/app.min.js", "~/assets/js/vscustom-carousel.min.js", "~/assets/js/ajax-mail.js", "~/assets/js/main.js");
            });
        }
    }
}
