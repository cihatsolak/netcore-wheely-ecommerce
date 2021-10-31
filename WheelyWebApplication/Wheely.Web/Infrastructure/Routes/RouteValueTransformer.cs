using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wheely.Core.Entities.Concrete.Routes;
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
        public RouteValueTransformer(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IRedisService redisService)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            _redisService = redisService;
        }
        #endregion

        #region Methods
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            string slugUrl = values["slug"]?.ToString();
            if (string.IsNullOrWhiteSpace(slugUrl))
            {
                return values;
            }

            var routes = await _redisService.GetAsync<List<RouteValueTransform>>("routes");
            if (routes is null || !routes.Any())
            {
                return values;
            }

            var route = routes.FirstOrDefault(p => p.SlugUrl.Equals(slugUrl, StringComparison.OrdinalIgnoreCase) || p.CustomUrl?.Equals(slugUrl, StringComparison.OrdinalIgnoreCase) == true);
            if (route is null)
            {
                return values;
            }

            values["Controller"] = route.ControllerName;
            values["Action"] = route.ActionName;

            if (route.EntityId > 0)
            {
                values["id"] = route.EntityId;
            }

            return values;
        }
        #endregion
    }
}
