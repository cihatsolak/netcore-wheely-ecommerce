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
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            string url = values["url"]?.ToString();
            if (string.IsNullOrWhiteSpace(url))
            {
                return new ValueTask<RouteValueDictionary>(values);
            }

            var routes = _redisService.Get<List<RouteValueTransform>>("routes");
            if (routes is null || !routes.Any())
            {
                return new ValueTask<RouteValueDictionary>(values);
            }

            var route = routes.FirstOrDefault(p => p.SlugUrl.Equals(url, StringComparison.OrdinalIgnoreCase) || p.CustomUrl.Equals(url, StringComparison.OrdinalIgnoreCase));
            if (route is null)
            {
                return new ValueTask<RouteValueDictionary>(values);
            }

            values["Controller"] = route.ControllerName;
            values["Action"] = route.ActionName;

            if (route.EntityId > 0)
            {
                values["id"] = route.EntityId;
            }

            return new ValueTask<RouteValueDictionary>(values);
        }
        #endregion
    }
}
