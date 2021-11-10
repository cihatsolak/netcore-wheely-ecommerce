using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Data.Abstract.Repositories;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Concrete.Repositories.EntityFrameworkCore
{
    public sealed class RouteRepository : EfEntityRepositoryBase<RouteValueTransform>, IRouteRepository
    {
        #region Constructor
        public RouteRepository(WheelDbContext wheelDbContext) : base(wheelDbContext)
        {
        }
        #endregion
    }
}
