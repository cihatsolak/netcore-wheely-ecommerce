﻿namespace Wheely.Web.Infrastructure.Constants
{
    /// <summary>
    /// Style and script files to be bundled by smidge
    /// </summary>
    public static class SmidgeFile
    {
        public static readonly string[] CSSFiles = {
            "~/assets/css/app.min.css",
            "~/assets/css/fontawesome.min.css",
            "~/assets/css/style.css",
            "~assets/css/theme-color1.css",
            "~assets/css/custom.css"
        };

        public static readonly string[] JavaScriptFiles = {
            "~/assets/js/vendor/jquery-1.12.4.min.js",
            "~/assets/js/app.min.js",
            "~/assets/js/vscustom-carousel.min.js",
            "~/assets/js/main.js"
        };

        public static readonly string[] JqueryValidationJavascriptFiles =
        {
            "~/assets/js/jquery-validation/jquery.validate.min.js",
            "~/assets/js/jquery-validation/jquery.validate.unobtrusive.min.js"
        };
    }

    /// <summary>
    /// Bundling names expected by smidge
    /// </summary>
    public static class BundleNames
    {
        public const string BaseCssFile = "app-styles";
        public const string BaseJSFile = "app-scripts";
        public const string JqueryValidation = "app-jquery-validation-scripts";
    }
}
