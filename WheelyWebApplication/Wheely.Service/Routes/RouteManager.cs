using System.Collections.Generic;
using System.Linq;
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
        public IDataResult<List<RouteValueTransform>> GetRoutes()
        {
            _redisService.TryGetValue("routes", out List<RouteValueTransform> routes);
            if (routes is null || !routes.Any())
            {
                routes = _routeRepository.GetAll();
            }

            if (routes is null || !routes.Any())
            {
                return new ErrorDataResult<List<RouteValueTransform>>();
            }
            else
            {
                _redisService.Set("routes", routes);
            }

            return new SuccessDataResult<List<RouteValueTransform>>(routes);
        }
        #endregion
    }
}
