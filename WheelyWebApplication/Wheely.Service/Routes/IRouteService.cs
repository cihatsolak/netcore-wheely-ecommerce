namespace Wheely.Service.Routes
{
    public partial interface IRouteService
    {
        Task<IDataResult<List<RouteValueTransform>>> GetRoutesAsync();
    }
}
