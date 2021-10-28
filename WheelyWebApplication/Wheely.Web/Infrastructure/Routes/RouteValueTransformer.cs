using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace Wheely.Web.Infrastructure.Routes
{
    internal sealed class RouteValueTransformer : DynamicRouteValueTransformer
    {
        #region Fields 
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;
        #endregion

        #region Constructor
        public RouteValueTransformer(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }
        #endregion

        #region Methods
        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            string url = values["FriendlyUrl"]?.ToString();
            if (string.IsNullOrWhiteSpace(url))
            {
                values["Controller"] = "Home";
                values["Action"] = "Index";

                return new ValueTask<RouteValueDictionary>(values);
            }

            return new ValueTask<RouteValueDictionary>(values);
        }
        #endregion
    }
}
