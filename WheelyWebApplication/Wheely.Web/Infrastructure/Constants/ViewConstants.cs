namespace Wheely.Web.Infrastructure.Constants
{
    /// <summary>
    /// Partial views names to be used by the web
    /// </summary>
    public static class PartialViewName
    {
        public const string HeaderPartial = "~/Views/Shared/PartialViews/Layout/_HeaderPartial.cshtml";
        public const string ScriptPartial = "~/Views/Shared/PartialViews/Layout/_ScriptPartial.cshtml";
        public const string BreadcumbPartial = "~/Views/Shared/PartialViews/Layout/_BreadcumbPartial.cshtml";
        public const string ValidationScriptsPartial = "~/Views/Shared/PartialViews/Validation/_ValidationScriptsPartial.cshtml";
        public const string ValidationBootstrapPartial = "~/Views/Shared/PartialViews/Validation/_ValidationBootstrapPartial.cshtml";
        public const string ValidationGoogleReCaptchaPartial = "~/Views/Shared/PartialViews/Validation/_ValidationGoogleCaptchaPartial.cshtml";
    }

    /// <summary>
    /// Partial view titles 
    /// </summary>
    public static class ViewTitle
    {
        public const string ShopDetail = "Tekerlek Detayı";
        public const string ContactIndex = "İletişim";
    }

    /// <summary>
    /// View data parameters
    /// </summary>
    public static class ViewDataParameter
    {
        public const string Title = "Title";
    }
}
