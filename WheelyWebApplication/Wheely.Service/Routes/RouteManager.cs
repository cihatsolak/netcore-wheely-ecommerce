namespace Wheely.Service.Routes
{
    public sealed class RouteManager : IRouteService
    {
        #region Fields
        private readonly IRouteRepository _routeRepository;
        private readonly IRedisService _redisService;
        #endregion

        #region Constructor
        public RouteManager(
            IRouteRepository routeRepository, 
            IRedisService redisService)
        {
            _routeRepository = routeRepository;
            _redisService = redisService;
        }
        #endregion

        #region Methods
        public async Task<IDataResult<List<RouteValueTransform>>> GetRoutesAsync()
        {
            IResult result = _redisService.TryGetValue(CacheKeyConstants.Routes, out List<RouteValueTransform> routes);
            if (result.Succeeded)
            {
                return new SuccessDataResult<List<RouteValueTransform>>(routes);
            }

            routes = await _routeRepository.TableNoTracking.ToListAsync();
            if (routes.IsNullOrNotAny())
            {
                return new ErrorDataResult<List<RouteValueTransform>>();
            }

            await _redisService.SetAsync(CacheKeyConstants.Routes, routes);
            return new SuccessDataResult<List<RouteValueTransform>>(routes);
        }
        #endregion
    }
}
