using Microsoft.AspNetCore.Routing;
using System.Net;
using Wheely.Core.Constants;
using Wheely.Core.Utilities;

namespace Wheely.Service.Routes.RouteValueTransforms
{
    public sealed class RouteValueTransformManager : IRouteValueTransformService
    {
        public bool CheckSlugUrl(RouteValueDictionary values, out string slugUrl)
        {
            slugUrl = values["slug"]?.ToString();

            if (string.IsNullOrWhiteSpace(slugUrl))
                return false;

            if (slugUrl.Equals("/") || slugUrl.Contains("."))
                return false;

            return true;
        }

        public RouteValueDictionary RedirectNotFoundPage(RouteValueDictionary values)
        {
            values[RouteName.Controller] = ControllerName.Error;
            values[RouteName.Action] = ActionName.Handle;
            values[RouteParameterName.StatusCode] = HttpStatusCode.NotFound.ToInt();

            return values;
        }
    }
}
