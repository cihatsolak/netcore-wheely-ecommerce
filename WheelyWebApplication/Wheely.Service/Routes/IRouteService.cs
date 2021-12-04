using System.Collections.Generic;
using System.Threading.Tasks;
using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Core.Services.Results.Abstract;

namespace Wheely.Service.Routes
{
    public partial interface IRouteService
    {
        Task<IDataResult<List<RouteValueTransform>>> GetRoutesAsync();
    }
}
