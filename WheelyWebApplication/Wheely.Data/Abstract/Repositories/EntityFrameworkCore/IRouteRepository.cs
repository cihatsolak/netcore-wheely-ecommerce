using Wheely.Core.Entities.Concrete.Routes;
using Wheely.Data.Concrete.Contexts;

namespace Wheely.Data.Abstract.Repositories.EntityFrameworkCore
{
    public partial interface IRouteRepository : IEntityRepository<RouteValueTransform, WheelDbContext>
    {
    }
}
