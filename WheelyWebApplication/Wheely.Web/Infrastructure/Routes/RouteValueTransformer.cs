using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Wheely.Core.Constants;
using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Core.Utilities;
using Wheely.Service.Redis;

namespace Wheely.Web.Infrastructure.Routes
{
    internal sealed class RouteValueTransformer : DynamicRouteValueTransformer
    {
        #region Fields 
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IRedisService _redisService;
        #endregion

        #region Constructor
        public RouteValueTransformer(
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, 
            IRedisService redisService)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _redisService = redisService;
        }
        #endregion

        #region Methods
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (!CheckSlugUrl(values, out string slugUrl)) return values;

            var routes = await _redisService.GetAsync<List<RouteValueTransform>>(CacheKeyConstants.Routes);
            if (routes is null || !routes.Any())
            {
                return values;
            }

            var route = routes.FirstOrDefault(p => p.SlugUrl.Equals(slugUrl, StringComparison.OrdinalIgnoreCase) || p.CustomUrl?.Equals(slugUrl, StringComparison.OrdinalIgnoreCase) == true);
            if (route is null)
            {
                return RedirectNotFoundPage(values);
            }

            values[RouteName.Controller] = route.ControllerName;
            values[RouteName.Action] = route.ActionName;

            if (route.EntityId > 0)
            {
                values[RouteParameterName.Id] = route.EntityId;
            }

            return values;
        }
        #endregion

        #region Utilities
        private static bool CheckSlugUrl(RouteValueDictionary values, out string slugUrl)
        {
            slugUrl = values["slug"]?.ToString();

            if (string.IsNullOrWhiteSpace(slugUrl))
                return false;

            if (slugUrl.Equals("/") || slugUrl.Contains("."))
                return false;

            return true;
        }

        private static RouteValueDictionary RedirectNotFoundPage(RouteValueDictionary values)
        {
            values[RouteName.Controller] = ControllerName.Error;
            values[RouteName.Action] = ActionName.Handle;
            values[RouteParameterName.StatusCode] = HttpStatusCode.NotFound.ToInt();

            return values;
        }
        #endregion
    }
}
