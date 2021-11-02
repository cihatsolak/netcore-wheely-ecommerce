using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wheely.Core.Constants;
using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Service.Redis;
using Wheely.Service.Routes.RouteValueTransforms;

namespace Wheely.Web.Infrastructure.Routes
{
    internal sealed class RouteValueTransformer : DynamicRouteValueTransformer
    {
        #region Fields 
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        private readonly IRedisService _redisService;
        private readonly IRouteValueTransformService _routeValueTransformService;
        #endregion

        #region Constructor
        public RouteValueTransformer(
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, 
            IRedisService redisService, 
            IRouteValueTransformService routeValueTransformService)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _redisService = redisService;
            _routeValueTransformService = routeValueTransformService;
        }
        #endregion

        #region Methods
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            if (!_routeValueTransformService.CheckSlugUrl(values, out string slugUrl)) return values;

            var routes = await _redisService.GetAsync<List<RouteValueTransform>>(CacheKeyConstants.Routes);
            if (routes is null || !routes.Any())
            {
                return values;
            }

            var route = routes.FirstOrDefault(p => p.SlugUrl.Equals(slugUrl, StringComparison.OrdinalIgnoreCase) || p.CustomUrl?.Equals(slugUrl, StringComparison.OrdinalIgnoreCase) == true);
            if (route is null)
            {
                return _routeValueTransformService.RedirectNotFoundPage(values);
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
    }
}
