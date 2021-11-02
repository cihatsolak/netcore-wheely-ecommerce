using Microsoft.AspNetCore.Routing;

namespace Wheely.Service.Routes.RouteValueTransforms
{
    public partial interface IRouteValueTransformService
    {
        bool CheckSlugUrl(RouteValueDictionary values, out string slugUrl);
        RouteValueDictionary RedirectNotFoundPage(RouteValueDictionary values);
    }
}
