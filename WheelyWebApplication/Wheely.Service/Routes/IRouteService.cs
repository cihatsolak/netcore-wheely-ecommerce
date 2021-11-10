using System.Collections.Generic;
using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.Routes
{
    public partial interface IRouteService
    {
        IDataResult<List<RouteValueTransform>> GetRoutes();
    }
}
