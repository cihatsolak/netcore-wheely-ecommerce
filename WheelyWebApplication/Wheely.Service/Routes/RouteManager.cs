using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wheely.Core.Constants;
using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Core.Services.Results.Abstract;
using Wheely.Core.Services.Results.Concrete;
using Wheely.Data.Abstract.Repositories;
using Wheely.Service.Redis;

namespace Wheely.Service.Routes
{
    public sealed class RouteManager : IRouteService
    {
        #region Fields
        private readonly IRouteRepository _routeRepository;
        private readonly IRedisService _redisService;
        #endregion

        #region Constructor
        public RouteManager(IRouteRepository routeRepository, IRedisService redisService)
        {
            _routeRepository = routeRepository;
            _redisService = redisService;
        }
        #endregion

        #region Methods
        public async Task<IDataResult<List<RouteValueTransform>>> GetRoutesAsync()
        {
            IResult result = _redisService.TryGetValue(CacheKeyConstants.Routes, out List<RouteValueTransform> routes);
            if (result.Success)
            {
                return new SuccessDataResult<List<RouteValueTransform>>(routes);
            }

            routes = await _routeRepository.TableNoTracking.AsNoTracking().ToListAsync();
            if (routes is null || !routes.Any())
            {
                return new ErrorDataResult<List<RouteValueTransform>>();
            }

            await _redisService.SetAsync(CacheKeyConstants.Routes, routes);
            return new SuccessDataResult<List<RouteValueTransform>>(routes);
        }
        #endregion
    }
}
